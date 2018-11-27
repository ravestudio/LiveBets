using System;
using System.Collections.Generic;
using System.Text;
using CommonLib;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace EventCollector
{
    public class Collector
    {
        private WebApiClient webApiClient = null;

        private string _apiUrl = "http://127.0.0.1:1255";

        public Collector(WebApiClient webApiClient)
        {
            this.webApiClient = webApiClient;
        }

        public void Start()
        {
            EventCreator eventCreator = new EventCreator();

            string body = "{\"limit\": 40,\"ns\":\"live\",\"widgetLmt\":false,\"widgetVideo\":false,\"sportIds\":[31,33]}";
            //string body = "{\"limit\": 40,\"ns\":\"live\",\"widgetLmt\":false,\"widgetVideo\":false,\"sportIds\":[34]}";

            System.Timers.Timer timer = new System.Timers.Timer(1000);

            while(true)
            {
               
                HttpContent content = new System.Net.Http.StringContent(body, Encoding.UTF8, "application/json");

                var start_ticks = System.DateTime.Now.Ticks;

                var t = this.webApiClient.PostDataAsync("https://mow1-lds-api.ligastavok.ru/rest/events/v1/grouping", content);

                IList<CommonLib.Objects.Event> resultEvents = new List<CommonLib.Objects.Event>();

                t.Wait();

                var duration = System.TimeSpan.FromTicks(System.DateTime.Now.Ticks - start_ticks);

                JObject o = JObject.Parse(t.Result);

                var result = o["result"];

                foreach (var _sport in result)
                {
                    foreach (var _event in _sport["events"])
                    {
                        var w = _event["outcomesWinner"];

                        var status = _event["event"]["status"];

                        if (w.HasValues && status.ToObject<string>() != "not_started")
                        {
                            var objEvent = eventCreator.CreateEvent(_event);

                            resultEvents.Add(objEvent);
                          
                        }

                    }
                }

                string resBody = JsonConvert.SerializeObject(resultEvents);

                HttpContent resContent = new System.Net.Http.StringContent(resBody, Encoding.UTF8, "application/json");

                var sendTask = this.webApiClient.PostDataAsync(string.Format("{0}/api/event", _apiUrl), resContent);

                sendTask.Wait();

                CommonLib.Objects.updInfo updInfo = new CommonLib.Objects.updInfo()
                {
                    lastUpd = System.DateTime.Now,
                    updDuration = duration.Milliseconds
                };

                string updBody = JsonConvert.SerializeObject(updInfo);
                HttpContent updContent = new System.Net.Http.StringContent(updBody, Encoding.UTF8, "application/json");
                var updTask = this.webApiClient.PostDataAsync(string.Format("{0}/api/event", _apiUrl), updContent);

                updTask.Wait();

                Task.Delay(30*1000).Wait();
            }
        }
    }
}

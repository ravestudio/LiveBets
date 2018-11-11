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
        public Collector(WebApiClient webApiClient)
        {
            this.webApiClient = webApiClient;
        }

        public void Start()
        {
            EventCreator eventCreator = new EventCreator();

            string body = "{\"limit\": 40,\"ns\":\"live\",\"widgetLmt\":false,\"widgetVideo\":false,\"sportIds\":[31,33]}";

            System.Timers.Timer timer = new System.Timers.Timer(1000);

            while(true)
            {
               
                HttpContent content = new System.Net.Http.StringContent(body, Encoding.UTF8, "application/json");

                var t = this.webApiClient.PostDataAsync("https://mow1-lds-api.ligastavok.ru/rest/events/v1/grouping", content);

                IList<CommonLib.Objects.Event> resultEvents = new List<CommonLib.Objects.Event>();

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

                var sendTask = this.webApiClient.PostDataAsync("http://bk.xplatform.net/api/event", resContent);

                sendTask.Wait();

                Task.Delay(60*1000).Wait();
            }
        }
    }
}

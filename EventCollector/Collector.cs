﻿using System;
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
                
                Console.WriteLine("begin iteration");

                HttpContent content = new System.Net.Http.StringContent(body, Encoding.UTF8, "application/json");

                /* try
                {
                    this.webApiClient.PostData_t("https://mow1-lds-api.ligastavok.ru/rest/events/v1/grouping", content);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }*/

                var json = this.webApiClient.GetData("http://ligastavok.ru");

                IList<CommonLib.Objects.Event> resultEvents = new List<CommonLib.Objects.Event>();

                //Console.Clear();

                //string json = t.Result;

                JObject o = JObject.Parse(json.Result);

                var result = o["result"];

                foreach (var _sport in result)
                {
                    foreach (var _event in _sport["events"])
                    {
                        var w = _event["outcomesWinner"];


                        var status = _event["event"]["status"];
                        var status2 = _event["status"];

                        if (w.HasValues && status.ToObject<string>() != "not_started")
                        {
                            var objEvent = eventCreator.CreateEvent(_event);

                            resultEvents.Add(objEvent);

                            //Console.WriteLine(_event);

                            string strevent = _event.ToString();

                            var scores = _event["scores"]["total"];

                            Console.WriteLine("Event Id: {0}", objEvent.id);

                            Console.WriteLine("{0}: {1} {2}", objEvent.eventTitle, objEvent.team1, objEvent.team2);


                            Console.WriteLine("matchTime: {0}", objEvent.matchTime);

                            Console.WriteLine();

                            
                        }

                    }
                }

                /* string resBody = JsonConvert.SerializeObject(resultEvents);

                HttpContent resContent = new System.Net.Http.StringContent(resBody, Encoding.UTF8, "application/json");

                var sendTask = this.webApiClient.PostData("http://bk.xplatform.net/api/event", resContent);

                var resp = sendTask.Result;*/

                Task.Delay(60*1000).Wait();
            }
        }
    }
}

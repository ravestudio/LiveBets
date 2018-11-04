using System;
using System.Collections.Generic;
using System.Text;
using CommonLib;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            //body = body.Replace("\"", "\"\"");

            HttpContent content = new System.Net.Http.StringContent(body, Encoding.UTF8, "application/json");
            var t = this.webApiClient.PostData("https://mow1-lds-api.ligastavok.ru/rest/events/v1/grouping", content);

            string json = t.Result;

            JObject o = JObject.Parse(json);

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
                        //Console.WriteLine(_event);

                        string strevent = _event.ToString();

                        var scores = _event["scores"]["total"];

                        Console.WriteLine("Event Id: {0}", _event["id"]);

                        Console.WriteLine("{0}: {1} {2}", _event["event"]["eventTitle"], scores["ScoreTeam1"], scores["ScoreTeam2"]);
                        //Console.WriteLine(_event["event"]);
                        //Console.WriteLine("{0}", _event["outcomesWinner"]);

                        Console.WriteLine("matchTime: {0}", _event["event"]["matchTime"]);

                        var outcomes = _event["outcomesWinner"]["main"]["outcomes"];
                        Console.WriteLine("win1:{0} X:{1} win2:{2}", outcomes["_1"] != null ? outcomes["_1"]["value"] : "-", outcomes["x"] != null ? outcomes["x"]["value"] : "-", outcomes["_2"] != null ? outcomes["_2"]["value"] : "-");

                        Console.WriteLine();

                        eventCreator.CreateEvent(_event);
                    }

                }
            }
        }
    }
}

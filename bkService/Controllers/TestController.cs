using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Objects;
using EventCollector;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace bkService.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            IList<Event> eventList = new List<Event>();

            EventCreator eventCreator = new EventCreator();

            string body = "{\"limit\": 40,\"ns\":\"live\",\"widgetLmt\":false,\"widgetVideo\":false,\"sportIds\":[31,33]}";

            HttpContent content = new System.Net.Http.StringContent(body, Encoding.UTF8, "application/json");

            var client = new CommonLib.WebApiClient();

            var t = client.PostData("https://mow1-lds-api.ligastavok.ru/rest/events/v1/grouping", content);

            JObject o = JObject.Parse(t.Result);

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

                        eventList.Add(objEvent);
                    }
                }
            }



            return eventList;
        }
    }
}
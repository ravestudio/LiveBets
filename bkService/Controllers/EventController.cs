using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Objects;
using Newtonsoft.Json;

namespace bkService.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            IEnumerable<Event> eventList = null;

            using (var context = new DataAccess.bkContext())
            {
                eventList = context.Events.Select(ev => JsonConvert.DeserializeObject<Event>(ev.jsonData)).ToList();
            }

            return eventList;
        }

        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<Event> events)
        {

            using (var context = new DataAccess.bkContext())
            {
                IList<DataAccess.Event> eventRange = new List<DataAccess.Event>();

                foreach(Event _event in events)
                {
                    DataAccess.Event dbEvent = new DataAccess.Event();
                    dbEvent.EventId = _event.id;
                    dbEvent.jsonData = JsonConvert.SerializeObject(_event);
                    eventRange.Add(dbEvent);
                }

                context.Events.AddRange(eventRange);

                context.SaveChanges();
            }

            return Ok(1);
        }
    }
}
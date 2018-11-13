using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Objects;
using CommonLib.Analyzer;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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
                var currentEvents = context.Events.ToList();

                IList<DataAccess.Event> eventRange = new List<DataAccess.Event>();

                foreach (Event _event in events)
                {
                    var curr = currentEvents.SingleOrDefault(c => c.EventId == _event.id);

                    if (curr != null)
                    {
                        //update current
                        curr.jsonData = JsonConvert.SerializeObject(_event);
                    }
                    else
                    {
                        //create new
                        DataAccess.Event dbEvent = new DataAccess.Event();
                        dbEvent.EventId = _event.id;
                        dbEvent.jsonData = JsonConvert.SerializeObject(_event);
                        dbEvent.HasMessage = false;
                        eventRange.Add(dbEvent);
                    }
                }

                context.Events.AddRange(eventRange);

                var ids = events.Select(e => e.id);
                var eventToDelete = currentEvents.Select(c => c.EventId).Except(ids);

                //delete old
                if (eventToDelete.Count() > 0)
                {
                    context.Events.RemoveRange(currentEvents.Where(c => eventToDelete.Contains(c.EventId)));
                }

                context.SaveChanges();
            }

            using (var context = new DataAccess.bkContext())
            {
                //make messages
                var eventsToAnalys = context.Events.Where(e => e.HasMessage == false).ToList();

                DrawFootballStrategy drawFootball = new DrawFootballStrategy();
                DrawHockeyStrategy drawHockey = new DrawHockeyStrategy();

                IList<DataAccess.Event> toSend = new List<DataAccess.Event>();

                foreach (var ev in eventsToAnalys)
                {

                    var obj = JsonConvert.DeserializeObject<Event>(ev.jsonData);

                    //football
                    if (obj.gameId == 33 && drawFootball.Check(obj))
                    {
                        toSend.Add(ev);
                    }

                    //hockey
                    if (obj.gameId == 31 && drawHockey.Check(obj))
                    {
                        toSend.Add(ev);
                    }
                }

                if (toSend.Count >0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach(var ev in toSend)
                    {
                        var obj = JsonConvert.DeserializeObject<Event>(ev.jsonData);
                        sb.AppendLine(obj.eventTitle);
                        sb.AppendLine("Bet to Draw");
                        sb.AppendLine();

                        ev.HasMessage = true;
                    }

                    context.Messages.Add(new DataAccess.Message()
                    {
                        MessageBody = sb.ToString(),
                        Sent = false
                    });

                    context.SaveChanges();
                }

            }

            return Ok(1);
        }
    }
}
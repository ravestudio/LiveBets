using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Objects;

namespace bkService.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            IList<Event> eventList = null;

            eventList = new List<Event>();

            eventList.Add(new Event()
            {
                eventTitle = "ЦСК - СКА"
            });

            eventList.Add(new Event()
            {
                eventTitle = "Авангард - Динамо"
            });

            return eventList;
        }

        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<Event> events)
        {
            return Ok(1);
        }
    }
}
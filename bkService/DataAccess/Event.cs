using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bkService.DataAccess
{
    public class Event
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string jsonData { get; set; }
    }
}

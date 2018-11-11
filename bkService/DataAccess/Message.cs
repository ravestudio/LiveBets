using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bkService.DataAccess
{
    public class Message
    {
        public int Id { get; set; }

        public string MessageBody { get; set; }

        public bool Sent { get; set; } 
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLib.Objects;
using Microsoft.AspNetCore.Mvc;

namespace bkService.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            IEnumerable<Message> messageList = null;

            using (var context = new DataAccess.bkContext())
            {
                messageList = context.Messages.Where(m => m.Sent == false).Select(msg => new Message()
                {
                    id = msg.Id,
                    messageBody = msg.MessageBody
                }).ToList();
            }

            return messageList;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var context = new DataAccess.bkContext())
            {
                var msg = context.Messages.SingleOrDefault(m => m.Id == id);
                if (msg != null)
                {
                    context.Remove(msg);

                    context.SaveChanges();
                }
            }
        }
    }
}
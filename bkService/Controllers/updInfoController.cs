using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bkService.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace bkService.Controllers
{
    [Route("api/[controller]")]
    public class updInfoController : Controller
    {
        [HttpGet]
        public updInfo Get()
        {
            updInfo _updInfo = null;

            using (var context = new DataAccess.bkContext())
            {
                _updInfo = context.updInfo.SingleOrDefault();
            }

            return _updInfo;
        }

        [HttpPost]
        public IActionResult Post([FromBody] updInfo updInfo)
        {
            using (var context = new DataAccess.bkContext())
            {
                var _updInfo = context.updInfo.SingleOrDefault();

                if (_updInfo != null)
                {
                    _updInfo.lastUpd = updInfo.lastUpd;
                    _updInfo.updDuration = updInfo.updDuration;

                    context.SaveChanges();
                }

            }

            return Ok();
        }
    }
}
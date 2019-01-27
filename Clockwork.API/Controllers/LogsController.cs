using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;


namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var db = new ClockworkContext())
            {
                var x = db.CurrentTimeQueries.Select(p => new Log
                {
                    ClientIp = p.ClientIp,
                    CurrentTimeQueryId = p.CurrentTimeQueryId,
                    Time = p.Time,
                    TimeZoneId = p.TimeZoneId,
                    UTCTime = p.UTCTime
                }).OrderByDescending(p=>p.CurrentTimeQueryId).ToList();
                return Ok(x);
            }
        }

    }
}

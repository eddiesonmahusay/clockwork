using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        [HttpGet]
        public IActionResult Get(string timeZoneId = null)
        {
            string clientIp = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;

            CurrentTimeQuery currentTimeQuery = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = clientIp,
                Time = serverTime,
                TimeZoneId = timeZoneId
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(currentTimeQuery);
                db.SaveChanges();
            }

            Log log = new Log
            {
                ClientIp = currentTimeQuery.ClientIp,
                CurrentTimeQueryId = currentTimeQuery.CurrentTimeQueryId,
                Time = currentTimeQuery.Time,
                TimeZoneId = currentTimeQuery.TimeZoneId,
                UTCTime = currentTimeQuery.UTCTime
            };

            return Ok(log);
        }

    }
}

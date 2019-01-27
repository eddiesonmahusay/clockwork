using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class TimeZonesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tz = TimeZoneInfo.GetSystemTimeZones().Select(p => new {
                Id = p.Id,
                Name = p.DisplayName
            });
            return Ok(tz);
        }
    }
}

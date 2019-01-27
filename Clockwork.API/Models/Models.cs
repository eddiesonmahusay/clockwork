using System;
using Microsoft.EntityFrameworkCore;

namespace Clockwork.API.Models
{
    public class ClockworkContext : DbContext
    {
        public DbSet<CurrentTimeQuery> CurrentTimeQueries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=clockwork.db");
        }
    }

    public class CurrentTimeQuery
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        public string TimeZoneId { get; set; }
    }

    public class Log : CurrentTimeQuery
    {
        public string TimeZone
        {
            get
            {
                if(TimeZoneId != null)
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId).DisplayName;
                }
                return String.Empty;
            }
        }

        public DateTime LocalTime
        {
            get
            {
                if(TimeZoneId != null)
                {
                    TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                    return TimeZoneInfo.ConvertTime(this.Time, timeZoneInfo);
                }

                return Time;
            }
        }
    }
}

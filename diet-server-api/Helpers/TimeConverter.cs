using System;

namespace diet_server_api.Helpers
{
    public class TimeConverter
    {
        public static DateTime GetCurrentPolishTime() => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central European Standard Time");
    }
}
using System;

namespace Todo.Domain
{
    public static class DateTimeExtensions
    {
        public static string ConvertTo24HourFormatWithSeconds(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

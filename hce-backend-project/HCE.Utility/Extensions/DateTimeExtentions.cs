using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Extensions
{
    public static class DateTimeExtentions
    {
        public static string DefaultTimeFormat = "hh:mm tt";
        public static string DefaultDateFormat = "yyyy/MM/dd";
        public static string DefaultDateTimeFormat = "yyyy/MM/dd hh:mm tt";

        public static DateTime GetCurrentDateTime(this DateTime dateTime)
        {
            return DateTime.UtcNow;
        }

        public static DateTime AddWeeks(this DateTime currentDate, int weeks)
        {
            return currentDate.AddDays(weeks * 7);
        }

        public static string GetDateTimeByDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString(DefaultDateFormat);
        }

        public static string GetDateTimeByDateFormat(this DateTime dateTime, string format)
        {
            return string.IsNullOrEmpty(format) ? dateTime.ToString(DefaultDateFormat) : dateTime.ToString(format);
        }

        public static string GetDateTimeByDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString(DefaultDateTimeFormat);
        }

        public static string GetDateTimeByTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString(DefaultTimeFormat);
        }

        public static string GetTimeSpanByTimeFormat(this TimeSpan timeSpan, string format = null)
        {
            return string.IsNullOrEmpty(format) ? new DateTime(timeSpan.Ticks).ToString(DefaultTimeFormat) : new DateTime(timeSpan.Ticks).ToString(format);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.HelperOperation
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// This parses dates with dd/MM/yy HH:mm format or dd/MM/yyyy HH:mm only
        /// </summary>
        /// <returns>DateTime</returns>
        public static string UnifyDateFormat(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;

            bool isParsale = DateTime.TryParse(date, out DateTime parsedDate);
            if (isParsale)
                return parsedDate.ToString();

            string[] splitted = date.Trim().Split(' ');
            if (splitted.Length != 2)
                return date; // This might return unexpected formats

            string unFormattedDate = splitted[0];
            string unFormattedTime = splitted[1];

            var splittedUnFormatedDate = unFormattedDate.Split('/');
            int day = Convert.ToInt32(splittedUnFormatedDate[0]),
                   month = Convert.ToInt32(splittedUnFormatedDate[1]),
                   year = Convert.ToInt32(splittedUnFormatedDate[2]);

            if (year < 2000)
                year += 2000;

            var splittedUnFormattedTime = unFormattedTime.Split(':');
            int hours = Convert.ToInt32(splittedUnFormattedTime[0]),
                minutes = Convert.ToInt32(splittedUnFormattedTime[1]);

            return DateTime.Parse($"{day}/{month}/{year} {hours}:{minutes}").ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.HelperOperation
{
    public static class StringHelper
    {
        public static string FormatDate(this string dateInString)
        {
            var isValid = DateTime.TryParse(dateInString, out DateTime date);
            return isValid ? date.ToString("d/MM/yyyy") : dateInString;
        }
        public static string FormatHijriDate(this string dateInString)
        {
            bool inCorrectFormat = dateInString?.Length == 8;
            if (!inCorrectFormat)
                return dateInString;

            var year = dateInString.Substring(0, 4);
            var month = dateInString.Substring(4, 2);
            var day = dateInString.Substring(6, 2);

            return $"{day}/{month}/{year}";
        }
        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}

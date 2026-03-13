using System;
using System.Collections.Generic;
using System.Linq;

namespace IS.DocumenFormater.api.Factories
{
    public static class Utilities
    {
        private static Random random = new Random();
        private static int[] SupportedSizes = { 480, 960, 1280 };

        public static String RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1)) yield return day;
        }
        public static T TryConvertTo<T>(object input)
        {
            T result = default(T);
            try
            {
                result = (T)Convert.ChangeType(input, typeof(T));
            }
            catch { }
            return result;
        }
        public static bool IsAllowedMimeType(this string base64string)
        {
            bool rpta = false;
            if (string.IsNullOrWhiteSpace(base64string) || base64string.Length < 5)
            {
                rpta = false;
            }
            else
            {
                string data = base64string.Substring(0, 5);
                switch (data.ToUpper())
                {
                    case "JVBER":
                        //pdf
                        rpta = true;
                        break;
                    default:
                        //other types
                        rpta = false;
                        break;
                }
            }
            return rpta;
        }
    }
}

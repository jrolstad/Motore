using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Dates
{
    public static class DateTimeExtensions
    {
        public static Mdy ToMdy(this DateTime input)
        {
            return new Mdy
                       {
                           Year = input.Year,
                           Month = input.Month,
                           Day = input.Day,
                       };
        }

        public static long ToTimestamp(this DateTime dateTime)
        {
            const string fmt = "yyyyMMddHHmmssfff";
            var dateString = dateTime.ToUniversalTime().ToString(fmt);
            var result = Int64.Parse(dateString);
            return result;
        }
    }
}

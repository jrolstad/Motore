using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Dates
{
    public class DateUtils
    {
        public static long ToTimestamp(DateTime dateTime)
        {
            const string fmt = "yyyyMMddHHmmssfff";
            var dateString = dateTime.ToUniversalTime().ToString(fmt);
            var result = Int64.Parse(dateString);
            return result;
        }

        public static DateTime FromTimestamp(string input)
        {
            if ((String.IsNullOrWhiteSpace(input)) || (input.Length != 17))
            {
                throw new ArgumentOutOfRangeException(String.Format("The input value '{0}' must be exactly 17 digits.",
                                                                    input));

            }

            var yyyy = input.Substring(0, 4);
            var MM = input.Substring(4, 2);
            var dd = input.Substring(6, 2);
            var HH = input.Substring(8, 2);
            var mm = input.Substring(10, 2);
            var ss = input.Substring(12, 2);
            var fff = input.Substring(14, 3);

            var n_yyyy = int.Parse(yyyy);
            var n_MM = int.Parse(MM);
            var n_dd = int.Parse(dd);
            var n_HH = int.Parse(HH);
            var n_mm = int.Parse(mm);
            var n_ss = int.Parse(ss);
            var n_fff = int.Parse(fff);

            return new DateTime(n_yyyy, n_MM, n_dd, n_HH, n_mm, n_ss, n_fff);
        }

        public static DateTime FromTimestamp(long input)
        {
            var converted = input.ToString();
            return FromTimestamp(converted);
        }
    }
}

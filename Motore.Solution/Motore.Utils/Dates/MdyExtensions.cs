using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Dates
{
    public static class MdyExtensions
    {
        public static Mdy AddDays(this Mdy input, int days)
        {
            var actualDate = input.ToDate();
            var newDate = actualDate.AddDays(days);
            return newDate.ToMdy();
        }

        public static DateTime ToDate(this Mdy input)
        {
            return new DateTime(input.Year, input.Month, input.Day);
        }
    }
}

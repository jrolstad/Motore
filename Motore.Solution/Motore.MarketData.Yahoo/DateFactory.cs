using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Dates;

namespace Motore.MarketData.Yahoo
{
    public class DateFactory
    {
        public virtual Mdy ConvertDate(DateTime inputDate)
        {
            var mdy = new Mdy
                          {
                              Year = inputDate.Year,
                              Month = inputDate.Month - 1,
                              Day = inputDate.Day
                          };

            return mdy;
        }
    }
}

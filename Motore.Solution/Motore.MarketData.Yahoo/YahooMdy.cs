using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Assertions;
using Motore.Utils.Dates;

namespace Motore.MarketData.Yahoo
{
    public class YahooMdy : Mdy
    {
        public YahooMdy() : base()
        {
            
        }

        public YahooMdy(Mdy from) : this()
        {
            base.Year = from.Year;
            base.Month = from.Month;
            base.Day = from.Day;
        }

        public override int Month
        {
            get
            {
                return base.Month - 1;
            }
            set
            {
                Assert.Fail(() => ((value >= 0) && (value < 12)),
                            "The 'Month' property on a YahooMdy object accepts values between 0 and 11 inclusive.");
                base.Month = value + 1;
            }
        }
    }
}

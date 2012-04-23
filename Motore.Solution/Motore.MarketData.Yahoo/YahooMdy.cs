using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                base.Month = value;
            }
        }
    }
}

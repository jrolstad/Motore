using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.MarketData
{
    public class DailyInstrumentMarketData
    {
        public virtual DateTime Date { get; set; }
        public virtual string Identifier { get; set; }
        public virtual decimal ClosingPrice { get; set; }
        // need currency at some point
    }
}

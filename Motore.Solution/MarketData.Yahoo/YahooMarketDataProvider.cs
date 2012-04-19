using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Motore.MarketData;

namespace MarketData.Yahoo
{
    public class YahooMarketDataProvider : IMarketDataProvider
    {
        public virtual List<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

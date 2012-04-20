using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.MarketData.Yahoo
{
    public class YahooMarketDataProvider : IMarketDataProvider
    {
        public virtual List<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

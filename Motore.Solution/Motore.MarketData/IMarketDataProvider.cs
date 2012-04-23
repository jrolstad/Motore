using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.MarketData
{
    public interface IMarketDataProvider
    {
        IEnumerable<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request);
    }
}

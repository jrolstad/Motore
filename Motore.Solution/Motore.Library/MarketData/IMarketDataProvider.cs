using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.MarketData
{
    public interface IMarketDataProvider
    {
        List<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request);
    }
}

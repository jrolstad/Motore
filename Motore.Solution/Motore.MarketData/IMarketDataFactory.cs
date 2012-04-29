using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.MarketData
{
    public interface IMarketDataFactory
    {
        DailyInstrumentMarketData CreateDailyInstrumentMarketData(string input);
        DailyInstrumentMarketData CreateDailyInstrumentMarketData(string identifier, string input);
    }
}

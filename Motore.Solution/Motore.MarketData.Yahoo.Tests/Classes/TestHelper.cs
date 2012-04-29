using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Motore.TestHelpers;

namespace Motore.MarketData.Yahoo.Tests.Classes
{
    public class TestHelper
    {
        public static string GetSampleHistoricalCsvLine(decimal close = 123.55M, decimal adjustedClose = 123.55M)
        {
            var fmt =
                EmbeddedResourceHelper.GetText(
                    Assembly.GetExecutingAssembly(),
                    "Motore.MarketData.Yahoo.Tests.Resources.Yahoo.HistoricalStockData.SingleLineFormat.txt");

            var date = "2012-02-14";
            var open = 123.45M;
            var high = 123.99M;
            var low = 123.01M;
            var volume = 9876543;
            
            var actual = String.Format(fmt, date, open, high, low, close, volume, adjustedClose);
            return actual;

        }

    }
}

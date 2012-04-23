using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.TestHelpers.MarketData
{
    public static class MarketDataTestHelper
    {
        private static readonly List<string> _identifiers = new List<string> { "AMZN", "GOOG", "MSFT", "XOM", "T", "SBUX", "F", "AAPL", "UTX", "MMM", "BA", "CAT", "CVX", "PG", "MRK", "NFLX", "STI" };
        private static readonly Random _rand = new System.Random();

        public static string GetRandomIdentifer()
        {
            var count = _identifiers.Count;
            var rnd = _rand.Next(0, (count - 1));
            return _identifiers[rnd];
        }
    }
}

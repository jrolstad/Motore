using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.TestHelpers.MarketData;
using NUnit.Framework;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    [TestFixture]
    public class YahooMarketDataProviderTests
    {
        [Test]
        [Category("Integration")]
        public void GetMarketData_does_not_throw()
        {
            var provider = new YahooMarketDataProvider();
            var identifier = MarketDataTestHelper.GetRandomIdentifer();
            var startDate = DateTime.Now.AddDays(-60);
            var endDate = startDate.AddDays(10);
            var request = new InstrumentMarketDataRequest
                              {
                                  Identifier = identifier,
                                  StartDate = startDate,
                                  EndDate = endDate,
                              };
            var data = provider.GetMarketData(request);
        }
    }
}

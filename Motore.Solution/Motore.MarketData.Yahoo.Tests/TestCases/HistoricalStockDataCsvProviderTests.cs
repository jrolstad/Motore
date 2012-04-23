using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.TestHelpers.Dates;
using Motore.TestHelpers.MarketData;
using Motore.Utils.Dates;
using Motore.Utils.Exceptions.Web;
using NUnit.Framework;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    [TestFixture]
    public class HistoricalStockDataCsvProviderTests
    {
        [Test]
        [Category("Integration")]
        public void GetMarketData_does_not_throw()
        {
            var provider = new HistoricalStockDataCsvProvider();
            var identifier = MarketDataTestHelper.GetRandomIdentifer();
            var startDate = DateTime.Now.AddDays(-54);
            var endDate = startDate.AddDays(3);
            var request = new InstrumentMarketDataRequest
                              {
                                  Identifier = identifier,
                                  StartDate = startDate,
                                  EndDate = endDate,
                              };
            provider.GetMarketData(request);
        }

        [Test]
        public void ConvertDate_throws_when_date_is_null()
        {
            var provider = new HistoricalStockDataCsvProvider();
            DateTime? input = null;
            Assert.Throws<Exception>(()=>provider.ConvertDate(input));

        }

        [Test]
        public void GetMarketData_throws_when_StartDate_is_null()
        {
            var provider = new HistoricalStockDataCsvProvider();
            var request = new InstrumentMarketDataRequest
                              {
                                  Identifier = "MSFT",
                                  StartDate = null,
                                  EndDate = DateTime.Now.AddDays(-2),
                              };
            Assert.Throws<Exception>(() => provider.GetMarketData(request));

        }

        [Test]
        public void GetMarketData_throws_when_EndDate_is_null()
        {
            var provider = new HistoricalStockDataCsvProvider();
            var request = new InstrumentMarketDataRequest
            {
                Identifier = "MSFT",
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = null,
            };
            Assert.Throws<Exception>(() => provider.GetMarketData(request));

        }

        [Test]
        public void GetResults_throws_NotFoundException_for_DJIA()
        {
            // arrange
            var start = RandomDateHelper.GetRandomRecentDate();
            var startMdy = start.ToMdy();
            var endMdy = startMdy.AddDays(4);
            var sy = new YahooMdy(startMdy);
            var ey = new YahooMdy(endMdy);

            const string identifier = "DJIA";

            var provider = new HistoricalStockDataCsvProvider();

            // act

            // assert
            Assert.Throws<NotFoundException>(() => provider.GetResults(identifier, sy, ey));
        }

    }
}

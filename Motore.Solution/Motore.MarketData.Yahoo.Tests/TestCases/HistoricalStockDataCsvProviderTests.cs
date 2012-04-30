using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.MarketData.Yahoo.Tests.Classes;
using Motore.TestHelpers.Dates;
using Motore.TestHelpers.MarketData;
using Motore.Utils.Dates;
using Motore.Utils.Exceptions.Web;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    [TestFixture]
    public class HistoricalStockDataCsvProviderTests
    {
        [Test]
        public void ConvertCsvLines_only_processes_lines_that_begin_with_number()
        {
            // arrange
            const string identifier = "GOOG";
            const string header = "Date, etc.";
            const string row1 = "2012-04-15, etc.";
            var lines = new List<string> {header, row1};

            var factory = MockRepository.GenerateStrictMock<IMarketDataFactory>();
            factory.Expect(f => f.CreateDailyInstrumentMarketData(identifier, row1)).Repeat.Once();

            var provider = MockRepository.GenerateMock<HistoricalStockDataCsvProvider>();
            provider.Expect(p => p.MarketDataFactory).Return(factory);
            provider.Expect(p => p.IsDataRow(header)).Repeat.Once().Return(false);
            provider.Expect(p => p.IsDataRow(row1)).Repeat.Once().Return(true);
            provider.Expect(p => p.ConvertCsvLines(identifier, lines)).CallOriginalMethod(
                OriginalCallOptions.CreateExpectation);

            // act
            provider.ConvertCsvLines(identifier, lines);

            // assert
            factory.VerifyAllExpectations();
            provider.VerifyAllExpectations();

        }

        [Test]
        public void GetMarketData_returns_results_from_GetResults()
        {
            // arrange
            var request = new InstrumentMarketDataRequest {Identifier = "YHOO"};
            var expectedResults = new List<DailyInstrumentMarketData>();
            var provider = MockRepository.GenerateMock<HistoricalStockDataCsvProvider>();
            provider.Expect(p => p.GetMarketData(request)).CallOriginalMethod(OriginalCallOptions.CreateExpectation);
            provider.Expect(p => p.GetResults(null, null, null)).IgnoreArguments().Repeat.Once().Return(expectedResults);

            // act
            var actualResults = provider.GetMarketData(request);

            // assert
            Assert.That(actualResults, Is.EqualTo(expectedResults));

        }
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

        [Test]
        public void ConvertCsvLines_returns_empty_list_for_null_input()
        {
            // arrange
            var provider = new HistoricalStockDataCsvProvider();
            
            // act
            var results = provider.ConvertCsvLines("ticker", null);

            // assert
            Assert.That(results, Is.InstanceOf<IEnumerable<DailyInstrumentMarketData>>());
            Assert.That(results.Count(), Is.EqualTo(0));

        }

        [Test]
        public void ConvertCsvLines_returns_empty_list_for_zero_count_input()
        {
            // arrange
            var provider = new HistoricalStockDataCsvProvider();

            // act
            var results = provider.ConvertCsvLines("ticker", new List<string>());

            // assert
            Assert.That(results, Is.InstanceOf<IEnumerable<DailyInstrumentMarketData>>());
            Assert.That(results.Count(), Is.EqualTo(0));

        }

        [Test]
        public void ConvertCsvLines_returns_one_item_for_single_input_row()
        {
            // arrange
            const string identifier = "YHOO";
            string input = TestHelper.GetSampleHistoricalCsvLine(); 
            var provider = new HistoricalStockDataCsvProvider();

            // act
            var results = provider.ConvertCsvLines(identifier, new List<string> { input });

            // assert
            Assert.That(results, Is.InstanceOf<IEnumerable<DailyInstrumentMarketData>>());
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Identifier, Is.EqualTo(identifier));

        }
    }
}

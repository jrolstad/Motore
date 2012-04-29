using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Motore.MarketData.Yahoo.Tests.Classes;
using Motore.TestHelpers;
using Motore.Utils.Logging;
using NUnit.Framework;
using Rhino.Mocks;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    [TestFixture]
    public class YahooMarketDataFactoryTests
    {
        #region Unit Tests

        [Test]
        public void ConvertDate_throws_on_unexpected_format__forward_slashes()
        {
            // arrange
            const string input = "2012/04/15";
            var factory = new YahooMarketDataFactory();

            // assert
            Assert.Throws<Exception>(() => factory.ConvertDate(input));

        }

        [Test]
        public void ConvertDate_throws_on_unexpected_format__dots()
        {
            // arrange
            const string input = "2012.04.15";
            var factory = new YahooMarketDataFactory();

            // assert
            Assert.Throws<Exception>(() => factory.ConvertDate(input));

        }

        [Test]
        public void ConvertDate_correctly_converts_hyphenated_date()
        {
            // arrange
            const string input = "2011-04-15";
            var factory = new YahooMarketDataFactory();

            // act
            var date = factory.ConvertDate(input);

            // assert
            Assert.That(date.Year, Is.EqualTo(2011));
            Assert.That(date.Month, Is.EqualTo(4));
            Assert.That(date.Day, Is.EqualTo(15));

        }

        [Test]
        public void CreateDailyInstrumentMarketData_with_no_identifier_throws_not_implemented_exception()
        {
            // arrange
            var factory = new YahooMarketDataFactory();

            // assert
            Assert.Throws<NotImplementedException>(()=> factory.CreateDailyInstrumentMarketData("something"));

        }

        [Test]
        public void CreateDailyInstrumentMarketData_returns_correct_close()
        {
            // arrange
            const decimal close = 98.88M;
            const string identifier = "YHOO";
            var input = TestHelper.GetSampleHistoricalCsvLine(close: close);

            var factory = new YahooMarketDataFactory();

            // act
            var data = factory.CreateDailyInstrumentMarketData(identifier, input);

            // assert
            Assert.That(data.ClosingPrice, Is.EqualTo(close));
        }

        [Test]
        public void CreateDailyInstrumentMarketData_returns_correct_adjusted_close()
        {
            // arrange
            const decimal adjustedClose = 99.99M;
            const string identifier = "YHOO";
            var input = TestHelper.GetSampleHistoricalCsvLine(adjustedClose: adjustedClose);
            
            var factory = new YahooMarketDataFactory();

            // act
            var data = factory.CreateDailyInstrumentMarketData(identifier, input);

            // assert
            Assert.That(data.AdjustedClosingPrice, Is.EqualTo(adjustedClose));
        }

        [Test]
        public void CreateDailyInstrumentMarketData_returns_correct_identifier()
        {
            // arrange
            const string identifier = "YHOO";
            var input = TestHelper.GetSampleHistoricalCsvLine();

            var factory = new YahooMarketDataFactory();

            // act
            var data = factory.CreateDailyInstrumentMarketData(identifier, input);

            // assert
            Assert.That(data.Identifier, Is.EqualTo(identifier));
        }

        #endregion

        #region Helper Tests

        [Test]
        [Ignore]
        public void GetAllEmbeddedResourceNamesInAssembly()
        {
            // useful for discovering the actual path of embedded resources
            var names = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            if (names.Length <= 0)
            {
                System.Console.Out.WriteLine("No embeded resources were found.");
            }
            else
            {

                names.ToList().ForEach(x =>
                {
                    System.Console.Out.WriteLine(x);
                });
            }
        }

        #endregion

    }
}

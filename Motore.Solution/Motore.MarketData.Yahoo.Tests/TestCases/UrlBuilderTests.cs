using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Dates;
using NUnit.Framework;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    public class UrlBuilderTests
    {
        [Test]
        public void BuildCsv_builds_correctly_for_single_day()
        {
            // arrange
            var s = "GOOG";
            var start = new Mdy {Month = 0, Day = 4, Year = 2011};
            var end = new Mdy {Month = 0, Day = 4, Year = 2011};
            var expected = "http://ichart.yahoo.com/table.csv?s=GOOG&a=0&b=4&c=2011&d=0&e=4&f=2011&g=d&ignore=.csv";
            var builder = new UrlBuilder();

            // act
            var actual = builder.BuildCsvUrl(s, start, end);

            // assert
            Assert.That(actual, Is.EqualTo(expected));

        }
    }
}

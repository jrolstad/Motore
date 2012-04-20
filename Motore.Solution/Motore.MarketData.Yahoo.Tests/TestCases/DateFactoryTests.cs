using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Motore.MarketData.Yahoo.Tests.TestCases
{
    [TestFixture]
    public class DateFactoryTests
    {
        [Test]
        public void ConvertDate_converts_day_correctly()
        {
            // arrange
            var input = new DateTime(1970, 12, 3);
            var expected = 3;

            // act

            var mdy = (new DateFactory()).ConvertDate(input);

            // assert
            Assert.That(mdy.Day, Is.EqualTo(expected));

        }

        [Test]
        public void ConvertDate_converts_January_correctly()
        {
            // arrange
            var input = new DateTime(1969, 1, 3);
            var expected = 0;

            // act

            var mdy = (new DateFactory()).ConvertDate(input);

            // assert
            Assert.That(mdy.Month, Is.EqualTo(expected));

        }

        [Test]
        public void ConvertDate_converts_December_correctly()
        {
            // arrange
            var input = new DateTime(1969, 12, 31);
            var expected = 11;

            // act

            var mdy = (new DateFactory()).ConvertDate(input);

            // assert
            Assert.That(mdy.Month, Is.EqualTo(expected));

        }

        [Test]
        public void ConvertDate_converts_pre_1970_year_correctly()
        {
            // arrange
            var input = new DateTime(1969, 5, 6);
            var expected = 1969;

            // act

            var mdy = (new DateFactory()).ConvertDate(input);

            // assert
            Assert.That(mdy.Year, Is.EqualTo(expected));

        }
    }

}

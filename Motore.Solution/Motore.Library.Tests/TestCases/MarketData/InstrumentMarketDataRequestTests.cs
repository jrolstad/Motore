using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.MarketData
{
    [TestFixture]
    public class InstrumentMarketDataRequestTests
    {
        [Test]
        public virtual void RequestId_defaults_to_today_timestamp()
        {
            // arrange
            var expectedBeginning = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd");

            // act

            var req = new InstrumentMarketDataRequest();

            // assert
            var id = req.RequestId;
            Assert.That(id.Length, Is.EqualTo(17));

            var actualBeginning = id.Substring(0, 8);
            Assert.That(actualBeginning, Is.EqualTo(expectedBeginning));
        }
    }
}

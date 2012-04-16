using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.MarketData
{
    [TestFixture]
    public class MarketDataRequestTests
    {
        [Test]
        public virtual void RequestId_defaults_to_today_timestamp()
        {
            // arrange
            var expected = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd");

            // act

            var req = new InstrumentMarketDataRequest();

            // assert
            var id = req.RequestId;
            var start = id.Substring(0, 8);
            NUnit.Framework.Assert.That(start, Is.EqualTo(expected));
        }
    }
}

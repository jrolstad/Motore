using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.MarketData
{
    [TestFixture]
    public class MarketDataRequestProcessorTests
    {
        [Test]
        public void Constructor_does_not_throw()
        {
            var processor = new MarketDataRequestProcessor();
        }
    }
}

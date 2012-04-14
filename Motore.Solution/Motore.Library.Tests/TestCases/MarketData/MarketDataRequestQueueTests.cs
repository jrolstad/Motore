using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using NUnit.Framework;
using Rhino.Mocks;

namespace Motore.Library.Tests.TestCases.MarketData
{
    [TestFixture]
    public class MarketDataRequestQueueTests
    {
        [Test]
        [Category("Integration")]
        public void Add_does_not_throw_exception()
        {
            // arrange
            var queue = new MarketDataRequestQueue();
            var request = new MarketDataRequest();

            // act
            queue.Add(request);
        }

        [Test]
        public void Add_calls_GetClient()
        {
            // arrange
            var queue = MockRepository.GeneratePartialMock<MarketDataRequestQueue>();
            queue.Expect(q => q.GetClient()).Repeat.Once();
            var request = MockRepository.GenerateStub<MarketDataRequest>();
            
            // act
            queue.Add(request);

            // assert
            queue.VerifyAllExpectations();
        }
    }
}

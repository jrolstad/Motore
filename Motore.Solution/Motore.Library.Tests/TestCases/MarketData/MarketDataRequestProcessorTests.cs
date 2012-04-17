using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

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

        [Test]
        public void Process_gets_info_from_queue()
        {
            // arrange
            var processor = MockRepository.GenerateMock<MarketDataRequestProcessor>();
            var queue = MockRepository.GenerateMock<MarketDataRequestQueue>();
            var list = new List<CombinedMarketDataRequest>();
            queue.Expect(q => q.GetRequests()).Return(list);
            processor.Expect(p => p.MarketDataRequestQueue).Repeat.Once().Return(queue);
            
            // act
            processor.Process();

            // assert
            processor.VerifyAllExpectations();

        }

        [Test]
        public void ProcessRequest_gets_reference_to_market_data_provider()
        {
            var processor = MockRepository.GenerateMock<MarketDataRequestProcessor>();
            var request = MockRepository.GenerateStub<InstrumentMarketDataRequest>();
            var provider = MockRepository.GenerateStub<IMarketDataProvider>();
            processor.Expect(p => p.GetMarketDataProvider(request)).Return(provider);
            processor.Expect(p => p.ProcessRequest(request)).CallOriginalMethod(OriginalCallOptions.CreateExpectation);

            // act
            processor.ProcessRequest(request);

            // assert
            processor.VerifyAllExpectations();

        }
    }
}

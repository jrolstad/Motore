using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using Motore.Library.Exceptions;
using Motore.Library.MarketData;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

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
            var request = new InstrumentMarketDataRequest
                              {
                                  Identifier = "SBUX",
                                  StartDate = DateTime.Now.AddDays(-1),
                                  EndDate = DateTime.Now.AddDays(-1),
                              };

            // act
            queue.Add(request);
        }


        [Test]
        public void Add_throws_if_request_is_null()
        {
            // arrange
            var queue = new MarketDataRequestQueue();
            
            // act
            Assert.Throws<ArgumentNullException>(() => queue.Add(null));

        }

        [Test]
        public void Add_throws_if_identifier_is_null()
        {
            // arrange
            var queue = new MarketDataRequestQueue();
            var request = new InstrumentMarketDataRequest
            {
                Identifier = null,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(-1),
            };

            // act
            Assert.Throws<Exception>(() => queue.Add(request));

        }

        [Test]
        public void Convert_throws_PossiblyPoisonMessageException_if_cant_deserialize()
        {
            var queue = new MarketDataRequestQueue();
            var message = new Amazon.SQS.Model.Message
                              {
                                  Body = "some garbage",
                              };

            Assert.Throws<PossiblyPoisonMessageException>(() => queue.Convert(message));
        }

        [Test]
        public void PossiblyPoisonMessageException_contains_original_messageid()
        {
            var messageId = "baz bat";
            var queue = new MarketDataRequestQueue();
            var message = new Amazon.SQS.Model.Message
            {
                Body = "some garbage",
                MessageId = messageId,
            };

            try
            {
                queue.Convert(message);
            }
            catch (PossiblyPoisonMessageException ppme)
            {
                Assert.That(ppme.MessageId, Is.EqualTo(messageId));
                return;
            }

            Assert.Fail("Should not have gotten this far in the test!");

        }
    }
}

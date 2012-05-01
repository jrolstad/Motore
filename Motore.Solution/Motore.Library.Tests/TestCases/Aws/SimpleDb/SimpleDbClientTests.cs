using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Entities;
using Motore.Utils.Dates;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.SimpleDb
{
    [TestFixture]
    public class SimpleDbClientTests
    {
        [Test]
        [Category("Integration")]
        public void ListDomains_does_not_throw()
        {
            var client = AwsClientFactory.CreateSimpleDbClient();
            client.ListDomains();
        }
        
        [Test]
        public void SaveEntity_does_not_throw_for_PortfolioCalculationRequest()
        {
            var timestamp = DateTime.Now.ToTimestamp();
            var entity = new PortfolioCalculationRequest
                             {
                                 RequestId = Guid.NewGuid().ToString(),
                                 CreatedBy = "Anthony",
                                 ModifiedBy = "Anthony",
                                 CreateTimestamp = timestamp,
                                 ModifyTimestamp = timestamp,
                             };

            var client = AwsClientFactory.CreateSimpleDbClient();
            client.SaveEntity<PortfolioCalculationRequest>(entity);
        }

        [Test]
        public void CreatePutAttributesRequest_returns_correct_value_for_PortfolioCalculationRequest()
        {
            // arrange
            var timestamp = DateTime.Now.ToTimestamp();
            var entity = new PortfolioCalculationRequest
            {
                RequestId = Guid.NewGuid().ToString(),
                CreatedBy = "Anthony",
                ModifiedBy = "Anthony",
                CreateTimestamp = timestamp,
                ModifyTimestamp = timestamp,
            };

            var client = AwsClientFactory.CreateSimpleDbClient();

            // act
            var request = client.CreatePutAttributesRequest<PortfolioCalculationRequest>(entity);

            // assert
            throw new NotImplementedException();
        }
        
    }
}

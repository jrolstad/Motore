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
            var guid = Guid.NewGuid().ToString();
            var entity = new PortfolioCalculationRequest
            {
                RequestId = guid,
                CreatedBy = "Anthony",
                ModifiedBy = "Anthony",
                CreateTimestamp = timestamp,
                ModifyTimestamp = timestamp,
            };

            var client = AwsClientFactory.CreateSimpleDbClient();

            // act
            var request = client.CreatePutAttributesRequest<PortfolioCalculationRequest>(entity);

            // assert
            Assert.IsNotNull(request.Attribute);
            Assert.That(request.Attribute.First(x => x.Name == "RequestId").Value, Is.EqualTo(guid));
        }

        [Test]
        public void CreatePutAttributesRequest_sets_clientip_to_empty_string_if_null()
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
                ClientIp = null,
            };

            var client = AwsClientFactory.CreateSimpleDbClient();

            // act
            var request = client.CreatePutAttributesRequest<PortfolioCalculationRequest>(entity);
            var attributeValue = request.Attribute.First(x => x.Name == "ClientIp").Value;

            // assert
            Assert.That(attributeValue, Is.EqualTo(""));
        }
        
    }
}

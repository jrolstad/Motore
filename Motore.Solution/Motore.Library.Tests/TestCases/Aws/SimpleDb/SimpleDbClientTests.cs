using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Utils.Dates;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.SimpleDb
{
    [TestFixture]
    public class SimpleDbClientTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var initializer = new DomainInitializer();
            initializer.Initialize();
        }

        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void ListDomains_does_not_throw()
        {
            var client = AwsClientFactory.CreateSimpleDbClient();
            client.ListDomains();
        }
        
        [Test]
        [Category("Integration")]
        [Category("AWS")]
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
                                 Origin = "TEST"
                             };

            var client = AwsClientFactory.CreateSimpleDbClient();
            client.SaveEntity<PortfolioCalculationRequest>(entity);
        }

        [Test]
        public void CreatePutAttributesRequest_returns_correct_value_for_non_primary_key_column()
        {
            // arrange
            var entity = new PortfolioCalculationRequest
            {
                CreatedBy = "Anthony",
            };

            var client = AwsClientFactory.CreateSimpleDbClient();

            // act
            var request = client.CreatePutAttributesRequest<PortfolioCalculationRequest>(entity);

            // assert
            Assert.IsNotNull(request.Attribute);
            Assert.That(request.Attribute.First(x => x.Name == "CreatedBy").Value, Is.EqualTo("Anthony"));
        }

        [Test]
        public void CreatePutAttributesRequest_sets_property_to_empty_string_if_null()
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

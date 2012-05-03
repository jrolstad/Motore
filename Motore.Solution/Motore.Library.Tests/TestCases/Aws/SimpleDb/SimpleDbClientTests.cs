using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Utils.Dates;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

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
        public void CreateSelectStatement_gets_domain_name_from_EntityHelper()
        {
            // arrange
            var client = MockRepository.GenerateMock<SimpleDbClient>(null, null);
            var helper = MockRepository.GenerateMock<SimpleDbEntityHelper>();
            client.Stub(c => c.EntityHelper).Return(helper);
            client.Expect(c => c.CreateSelectStatement<PortfolioCalculationRequest>(99)).Repeat.Once().CallOriginalMethod(
                OriginalCallOptions.CreateExpectation);

            // act
            client.CreateSelectStatement<PortfolioCalculationRequest>(99);

            // assert
            helper.AssertWasCalled(h=>h.GetDomainNameOfEntity<PortfolioCalculationRequest>());
        }

        [Test]
        public void Get_T_calls_CreateSelectRequest_T()
        {
            // arrange
            string nextToken = null;
            const string selectStatement = "foo bar bat";
            var client = MockRepository.GenerateMock<SimpleDbClient>(null, null);
            client.Expect(p => p.Get<PortfolioCalculationRequest>(2, ref nextToken)).Repeat.Once().CallOriginalMethod(
                OriginalCallOptions.CreateExpectation);
            // act
            client.Get<PortfolioCalculationRequest>(2, ref nextToken);

            // assert
            client.AssertWasCalled(x => x.CreateSelectRequest<PortfolioCalculationRequest>(2, nextToken));
            
        }

        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void Get_PortfolioCalculationRequests_does_not_throw()
        {
            // arrange
            var client = AwsClientFactory.CreateSimpleDbClient();
            string nextToken = null;

            // act
            client.Get<PortfolioCalculationRequest>(2, ref nextToken);
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

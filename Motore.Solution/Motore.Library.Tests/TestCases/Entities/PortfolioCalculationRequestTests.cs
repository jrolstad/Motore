using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Utils.Dates;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Entities
{
    [TestFixture]
    public class PortfolioCalculationRequestTests
    {
        [TearDown]
        public void TearDown()
        {
            SystemTime.Now = () => DateTime.Now;
        }

        [Test]
        public void RequestId_is_not_null_or_blank_for_new_object()
        {
            // arrange
            // act
            var request = new PortfolioCalculationRequest();
            
            // assert
            Assert.False(String.IsNullOrWhiteSpace(request.RequestId));
        }

        [Test]
        public void Default_constructor_sets_request_date_to_now()
        {
            // arrange
            var dt = new DateTime(2008, 3, 4, 2, 1, 3);
            SystemTime.Now = () => dt;

            // act
            var request = new PortfolioCalculationRequest();

            // assert
            Assert.That(request.RequestDate, Is.EqualTo(dt));
        }

        [Test]
        public void SimpleDbEntityHelper_reports_primary_key_is_RequestId()
        {
            var guid = Guid.NewGuid().ToString();
            var request = new PortfolioCalculationRequest {RequestId = guid};
            var helper = new SimpleDbEntityHelper();

            // act
            var actual = helper.GetPrimaryKeyValueOfEntity<PortfolioCalculationRequest>(request);

            // assert
            Assert.That(actual, Is.EqualTo(guid));
        }

        [Test]
        public void SimpleDbEntityHelper_reports_Domain_is_PortfolioCalculationRequest()
        {
            // arrange
            var expected = "PortfolioCalculationRequest";
            var request = new PortfolioCalculationRequest();
            var helper = new SimpleDbEntityHelper();

            // act
            var actual = helper.GetDomainNameOfEntity<PortfolioCalculationRequest>(request);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}

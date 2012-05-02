using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}

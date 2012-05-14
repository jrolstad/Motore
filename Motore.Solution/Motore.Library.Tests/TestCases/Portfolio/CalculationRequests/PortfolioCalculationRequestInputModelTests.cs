using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Models.Portfolio;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Portfolio.CalculationRequests
{
    [TestFixture]
    public class PortfolioCalculationRequestInputModelTests
    {
        [Test]
        public void PortfolioFileType_is_unknown()
        {
            // arrange
            var model = new PortfolioCalculationRequestInputModel();

            // act
            // assert
            Assert.That(model.PortfolioFileType, Is.EqualTo("unknown"));
        }

        [Test]
        public void RequestId_is_set()
        {
            // arrange
            var model = new PortfolioCalculationRequestInputModel();

            // act
            // assert
            Assert.False(String.IsNullOrWhiteSpace(model.RequestId));
        }
    }
}

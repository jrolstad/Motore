using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Models;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Models
{
    [TestFixture]
    public class HomeModelTests
    {
        [Test]
        public void PortfolioCalculationRequestInputModel_is_not_null()
        {
            // arrange
            // act
            var homeModel = new HomeModel();

            // assert
            Assert.IsNotNull(homeModel.PortfolioCalculationRequestInputModel);
        }
    }
}

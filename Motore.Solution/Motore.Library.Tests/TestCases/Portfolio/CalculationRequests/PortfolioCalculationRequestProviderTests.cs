using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Library.Models.Portfolio;
using Motore.Library.Portfolios.Requests;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Portfolio.CalculationRequests
{
    [TestFixture]
    public class PortfolioCalculationRequestProviderTests
    {
        [Test]
        public void Convert_correctly_converts_list()
        {
            // arrange
            var guid = Guid.NewGuid().ToString();
            var provider = new PortfolioCalculationRequestProvider();
            var inputList = new List<PortfolioCalculationRequest>
                                {
                                    new PortfolioCalculationRequest {RequestId = guid},
                                };
            // act
            var convertedList = provider.Convert(inputList).ToList();
            
            // assert
            Assert.That(convertedList, Is.TypeOf<List<PortfolioCalculationRequestViewModel>>());
            Assert.That(convertedList.Count, Is.EqualTo(1));
            Assert.That(convertedList.First().RequestId, Is.EqualTo(guid));

        }

        [Test]
        public void Convert_correctly_converts_RequestId()
        {
            // arrange
            var guid = Guid.NewGuid().ToString();
            var request = new PortfolioCalculationRequest {RequestId = guid};
            var provider = new PortfolioCalculationRequestProvider();

            // act
            var model = provider.Convert(request);

            // assert
            Assert.That(model.RequestId, Is.EqualTo(guid));
        }

        [Test]
        public void Convert_correctly_converts_RequestDate()
        {
            // arrange
            var requestDate = new DateTime(2011, 2, 3, 15, 3, 21);
            var request = new PortfolioCalculationRequest { RequestDate = requestDate  };
            var provider = new PortfolioCalculationRequestProvider();

            // act
            var model = provider.Convert(request);

            // assert
            Assert.That(model.RequestDate, Is.EqualTo(requestDate));
        }

        [Test]
        public void Convert_correctly_converts_Status()
        {
            // arrange
            var status = PortfolioCalculationRequestStatus.Error;
            var request = new PortfolioCalculationRequest { Status = status };
            var provider = new PortfolioCalculationRequestProvider();

            // act
            var model = provider.Convert(request);

            // assert
            Assert.That(model.Status, Is.EqualTo(status));
        }

        [Test]
        public void Convert_correctly_converts_PortfolioFileInfo()
        {
            // arrange
            var portfolioFileInfo = "foo bar bat";
            var request = new PortfolioCalculationRequest { PortfolioFileInfo = portfolioFileInfo };
            var provider = new PortfolioCalculationRequestProvider();

            // act
            var model = provider.Convert(request);

            // assert
            Assert.That(model.PortfolioFileInfo, Is.EqualTo(portfolioFileInfo));
        }
    }
}

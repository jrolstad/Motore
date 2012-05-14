using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Library.Models.Portfolio;
using Motore.Library.Portfolios.Requests;
using NUnit.Framework;
using Rhino.Mocks;

namespace Motore.Library.Tests.TestCases.Portfolio.CalculationRequests
{
    [TestFixture]
    public class PortfolioCalculationRequestProviderTests
    {
        [Test]
        public void GetS3PortfolioFileName_returns_correct_value()
        {
            // arrange
            var requestId = "foo";
            var portfolioFileType = "bar";
            var expected = "foo.bar.port";

            var input = MockRepository.GenerateStub<PortfolioCalculationRequestInputModel>();
            input.RequestId = requestId;
            input.PortfolioFileType = portfolioFileType;

            var provider = new PortfolioCalculationRequestProvider();

            // act
            var actual = provider.GetS3PortfolioFileName(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

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

        [Test]
        public void SubmitRequest_throws_if_input_is_null()
        {
            // arrange
            var provider = new PortfolioCalculationRequestProvider();

            // act
            // assert
            Assert.Throws<Exception>(() => provider.SubmitRequest(null));
        }

        [Test]
        public void SubmitRequest_throws_if_input_RequestId_is_null()
        {
            // arrange
            var provider = new PortfolioCalculationRequestProvider();
            var input = new PortfolioCalculationRequestInputModel {RequestId = null};

            // act
            // assert
            Assert.Throws<Exception>(() => provider.SubmitRequest(input));
        }
    }
}

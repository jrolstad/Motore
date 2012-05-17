using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.S3;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Library.Models.Portfolio;
using Motore.Library.Portfolios;
using Motore.Library.Portfolios.Requests;
using Motore.Utils.Dates;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Motore.Library.Tests.TestCases.Portfolio.CalculationRequests
{
    [TestFixture]
    public class PortfolioCalculationRequestProviderTests
    {
        [Test]
        public void ConvertToPortfolioFileInfo_sets_UploadTimestamp_from_current_time()
        {
            // arrange
            var dt = new DateTime(2011, 4, 1, 9, 33, 21);
            var ts = dt.ToTimestamp();
            SystemTime.Now = () => dt;
            var putInfo = new S3PutInfo();
            var provider = new PortfolioCalculationRequestProvider();

            // act

            var actual = provider.ConvertToPortfolioFileInfo(putInfo);

            // assert
            Assert.That(actual.UploadTimestamp, Is.EqualTo(ts));
        }

        [Test]
        public void SavePortfolioFile_calls_correct_methods()
        {
            // arrange
            var dt = new DateTime(2011, 4, 1, 9, 33, 21);
            SystemTime.Now = () => dt;
            var savePath = "foobarbat";
            var clientFileName = "zeezing";
            
            var putInfo = MockRepository.GenerateMock<S3PutInfo>();
            var fileInfo = MockRepository.GenerateMock<PortfolioFileInfo>();
            fileInfo.Expect(f => f.UploadTimestamp).Return(dt.ToTimestamp());

            var s3Client = MockRepository.GenerateStub<S3Client>(null, null);
            s3Client.Expect(s => s.Save(null, null, null)).IgnoreArguments().Return(putInfo);

            var model = MockRepository.GenerateStub<PortfolioCalculationRequestInputModel>();
            model.Expect(m=>m.ClientFileName).Return(clientFileName);

            var provider = MockRepository.GenerateMock<PortfolioCalculationRequestProvider>();
            provider.Expect(p => p.S3Client).Return(s3Client);
            provider.Expect(p => p.CalculatePortfolioFileSavePath(model)).Return(savePath);
            provider.Expect(p => p.ConvertToPortfolioFileInfo(putInfo)).Return(fileInfo);
            provider.Expect(p => p.SavePortfolioFile(model)).CallOriginalMethod(OriginalCallOptions.CreateExpectation);

            // act
            provider.SavePortfolioFile(model);

            // assert
            s3Client.VerifyAllExpectations();
            model.VerifyAllExpectations();
            provider.VerifyAllExpectations();

        }

        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void SaveUserFileRecord_saves_to_simpledb()
        {
            // arrange
            var provider = new PortfolioCalculationRequestProvider();
            var fileInfo = new PortfolioFileInfo
                               {
                                   ClientFileName = "client",
                                   FileSystemType = FileSystemType.S3,
                                   UploadTimestamp = SystemTime.Now().ToTimestamp(),
                                   Uri = "some test uri",
                               };
            // act
            var id = provider.SaveUserFileRecord("foo", fileInfo);

            // assert
            var client = AwsClientFactory.CreateSimpleDbClient();
            var actualEntity = client.Get<UserFile>(id, true);

            Assert.That(actualEntity.Location, Is.EqualTo("some test uri"));
        }

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
            var ts = requestDate.ToTimestamp();
            var request = new PortfolioCalculationRequest { RequestTimestamp = ts  };
            var provider = new PortfolioCalculationRequestProvider();

            // act
            var model = provider.Convert(request);

            // assert
            Assert.That(model.RequestTimestamp, Is.EqualTo(ts));
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

        [TearDown]
        public void TearDown()
        {
            SystemTime.Now = () => DateTime.Now;
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var di = new DomainInitializer();
            di.Initialize();
        }

    }
}

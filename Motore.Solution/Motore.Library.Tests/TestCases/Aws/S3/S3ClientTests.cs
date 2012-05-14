using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Configuration;
using Motore.Library.Tests.Classes;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.S3
{
    [TestFixture]
    public class S3ClientTests
    {
        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void Save_saves_file_to_S3()
        {
            // arrange
            var bucket = Config.PortfolioBucketName;
            var client = AwsClientFactory.CreateS3Client();
            var stream = TestHelper.GetEmbeddedResourceStream("S3.SampleFile1.txt");
            var path = "test-" + Guid.NewGuid().ToString();

            // act
            var info = client.Save(stream, bucket, path);

            // assert
            var exists = client.Exists(bucket, path);
            Assert.IsTrue(exists);
        }

        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void Exists_returns_false_for_made_up_path()
        {
            // arrange
            var bucket = Config.PortfolioBucketName;
            var path = Guid.NewGuid().ToString();
            var client = AwsClientFactory.CreateS3Client();
            
            // act
            var actual = client.Exists(bucket, path);

            // assert
            Assert.IsFalse(actual);

        }

        #region Helper Tests

        [Test]
        [Ignore]
        public void GetAllEmbeddedResourceNamesInAssembly()
        {
            // useful for discovering the actual path of embedded resources
            var names = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            if (names.Length <= 0)
            {
                System.Console.Out.WriteLine("No embeded resources were found.");
            }
            else
            {

                names.ToList().ForEach(x =>
                {
                    System.Console.Out.WriteLine(x);
                });
            }
        }

        #endregion

    }
}

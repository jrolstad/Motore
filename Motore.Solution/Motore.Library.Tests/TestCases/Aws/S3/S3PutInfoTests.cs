using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.S3;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.S3
{
    [TestFixture]
    public class S3PutInfoTests
    {
        [Test]
        public void Uri_is_constructed_as_expected()
        {
            // arrange
            var info = new S3PutInfo
                           {
                               Scheme = "a",
                               ServiceUrl = "b",
                               BucketName = "c",
                               Path = "d"
                           };

            var expected = "a://b/c/d";

            // act
            var actual = info.Uri;

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}

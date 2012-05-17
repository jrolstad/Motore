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
        public void Scheme_is_always_lowercase()
        {
            // arrange
            var info = new S3PutInfo {Scheme = "FOO"};
            // act
            var actual = info.Scheme;
            // assert
            Assert.That(actual, Is.EqualTo("foo"));
        }

        [Test]
        public void ServiceUrl_is_always_lowercase()
        {
            // arrange
            var info = new S3PutInfo {ServiceUrl = "BAR"};
            // act
            var actual = info.ServiceUrl;
            // assert
            Assert.That(actual, Is.EqualTo("bar"));
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws
{
    [TestFixture]
    public class AwsClientFactoryTests
    {
        [Test]
        public void CreateSimpleDbClient_returns_client()
        {
            // arrange
            // act
            var client = AwsClientFactory.CreateSimpleDbClient();

            // assert
            Assert.IsNotNull(client);
            Assert.IsInstanceOf<SimpleDbClient>(client);
        }
    }
}

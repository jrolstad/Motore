using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.SimpleDb
{
    [TestFixture]
    public class SimpleDbClientTests
    {
        [Test]
        [Category("Integration")]
        public void ListDomains_does_not_throw()
        {
            var client = AwsClientFactory.CreateSimpleDbClient();
            client.ListDomains();
        }
        
    }
}

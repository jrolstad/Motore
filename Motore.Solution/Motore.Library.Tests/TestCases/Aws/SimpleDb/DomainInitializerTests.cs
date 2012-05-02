using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.SimpleDb;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.SimpleDb
{
    [TestFixture]
    public class DomainInitializerTests
    {
        [Test]
        [Category("Integration")]
        [Category("AWS")]
        public void Initialize_does_not_throw()
        {
            var initializer = new DomainInitializer();
            initializer.Initialize();
        }
    }
}

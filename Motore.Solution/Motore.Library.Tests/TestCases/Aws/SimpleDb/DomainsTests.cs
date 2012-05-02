using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.SimpleDb;
using NUnit.Framework;

namespace Motore.Library.Tests.TestCases.Aws.SimpleDb
{
    [TestFixture]
    public class DomainsTests
    {
        [Test]
        public void ContainsName_returns_false_if_passed_null()
        {
            // arrange
            var domains = new Domains();

            // act
            var actual = domains.ContainsName(null);

            // assert
            Assert.False(actual);
        }

        [Test]
        public void ContainsName_returns_false_if_passed_empty_string()
        {
            // arrange
            var domains = new Domains {new Domain {Name = ""}};

            // act
            var actual = domains.ContainsName("");

            // assert
            Assert.False(actual);
        }

        [Test]
        public void ContainsName_is_case_insensitive()
        {
            // arrange
            var domains = new Domains { new Domain { Name = "FOO" } };

            // act
            var actual = domains.ContainsName("foo");

            // assert
            Assert.True(actual);
        }
    }
}

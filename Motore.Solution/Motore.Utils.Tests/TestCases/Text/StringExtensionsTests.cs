using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Text;
using NUnit.Framework;

namespace Motore.Utils.Tests.TestCases.Text
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void BeginsWithNumber_returns_false_for_alpha()
        {
            var input = "a123";
            var result = input.BeginsWithNumber();
            Assert.False(result);
        }

        [Test]
        public void BeginsWithNumber_returns_true_for_numeric()
        {
            var input = "1abc";
            var result = input.BeginsWithNumber();
            Assert.True(result);
        }
    }
}

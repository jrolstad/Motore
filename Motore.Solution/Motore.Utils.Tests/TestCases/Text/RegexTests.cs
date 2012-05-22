using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Text;
using NUnit.Framework;

namespace Motore.Utils.Tests.TestCases.Text
{
    [TestFixture]
    public class RegexTests
    {
        [Test]
        public virtual void GetValueBeforeFirstSlash_returns_empty_string_for_null_input()
        {
            // arrange
            string input = null;
            string expected = "";

            // act
            var actual = Regex.GetValueBeforeFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueBeforeFirstSlash_returns_empty_string_for_empty_input()
        {
            // arrange
            string input = "";
            string expected = "";

            // act
            var actual = Regex.GetValueBeforeFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueBeforeFirstSlash_returns_actual_value_if_no_slashes()
        {
            // arrange
            var input = " aas dfa asd fas fda asdf ";
            var expected = input;

            // act
            var actual = Regex.GetValueBeforeFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueBeforeFirstSlash_returns_correct_value_if_input_ends_with_slash()
        {
            // arrange
            var input = "asdfsda/";
            var expected = "asdfsda";

            // act
            var actual = Regex.GetValueBeforeFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueBeforeFirstSlash_returns_correct_value_for_one_slash()
        {
            // arrange
            var input = "zee/zip";
            var expected = "zee";

            // act
            var actual = Regex.GetValueBeforeFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_empty_string_for_null_input()
        {
            // arrange
            string input = null;
            string expected = "";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_empty_string_for_empty_input()
        {
            // arrange
            string input = "";
            string expected = "";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_actual_value_if_no_slashes()
        {
            // arrange
            var input = " aas dfa asd fas fda asdf ";
            var expected = input;

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_correct_value_if_input_starts_with_slash()
        {
            // arrange
            var input = "/asdfsda";
            var expected = "asdfsda";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_correct_value_for_one_slash()
        {
            // arrange
            var input = "zee/zip";
            var expected = "zip";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_correct_value_for_two_slash()
        {
            // arrange
            var input = "zee/zip/zoo";
            var expected = "zip/zoo";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public virtual void GetValueAfterFirstSlash_returns_correct_value_if_input_ends_with_slash()
        {
            // arrange
            var input = "zee/";
            var expected = "";

            // act
            var actual = Regex.GetValueAfterFirstSlash(input);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}

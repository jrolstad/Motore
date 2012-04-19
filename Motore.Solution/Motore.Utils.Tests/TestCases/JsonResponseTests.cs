using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Json;
using Motore.Utils.Serialization;
using NUnit.Framework;

namespace Motore.Utils.Tests.TestCases.UtilsTests
{
    [TestFixture]
    public class JsonResponseTests
    {
        [Test]
        public void successful_response_does_not_include_exception_field()
        {
            var response = new JsonResponse(true, "some message");
            var toString = response.SerializeToJson<JsonResponse>();
            Assert.That(toString.Contains("exception"), Is.False);
        }

        [Test]
        public void unsuccessful_response_does_include_exception_field()
        {
            var response = new JsonResponse(false, "some message");
            var toString = response.SerializeToJson<JsonResponse>();
            Assert.That(toString.Contains("exception"), Is.True);
        }
    }
}

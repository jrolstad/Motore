using System.Web.Mvc;
using Motore.Performance.Web.Controllers;
using NUnit.Framework;

namespace Motore.Performance.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class When_showing_the_landing_page
    {
        private ViewResult _result;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            _result = controller.Index();

        }

        [Test]
        public void Then_the_view_is_shown()
        {
            Assert.That(_result,Is.Not.Null);
        }
    }
}
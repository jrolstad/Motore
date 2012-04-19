using System.Linq;
using Directus.SimpleDb.Providers;
using Motore.Library.Models;
using Motore.Library.Programs;
using Motore.Utils.Entities;
using NUnit.Framework;
using Rhino.Mocks;
using Rolstad.System.Dates;

namespace Motore.Library.Tests.TestCases.Programs.AlphaProgramTests
{
    [TestFixture]
    public class When_saving_an_interested_customer
    {
        private string _emailAddress;
        private GenericResponse _result;
        private SimpleDBProvider<Customer, string> _dataProvider;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            // Arrange
            Clock.Freeze();

            _emailAddress = "IwantInfo@more.com";
            _dataProvider = MockRepository.GenerateStub<SimpleDBProvider<Customer, string>>();

            var program = new AlphaProgram(_dataProvider);

            // Act
            _result = program.SaveInterestedCustomer(_emailAddress);
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            Clock.Thaw();
        }

        [Test]
        public void Then_their_email_is_saved_to_simpledb()
        {
            _dataProvider.AssertWasCalled(p => p.Save(Arg<Customer[]>.Matches(c => c.Single().EmailAddress == _emailAddress)));
        }

        [Test]
        public void Then_the_current_time_is_saved_to_simpledb()
        {
            _dataProvider.AssertWasCalled(p => p.Save(Arg<Customer[]>.Matches(c => c.Single().CreateDate == Clock.Now)));
        }

        [Test]
        public void Then_the_customer_is_indicated_as_being_interested_in_the_alpha_program_is_saved_to_simpledb()
        {
            _dataProvider.AssertWasCalled(p => p.Save(Arg<Customer[]>.Matches(c => c.Single().IsAlphaCustomer == true)));
        }

        [Test]
        public void Then_the_response_is_successful()
        {
            Assert.That(_result.Success,Is.True);
        }

        [Test]
        public void Then_the_repsonse_has_a_thank_you_message()
        {
            Assert.That(_result.Message,Is.StringContaining("Thank You"));
        }

    }
}
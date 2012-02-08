using System.Web.Mvc;
using Motore.Library.Programs;
using Motore.Library.Utils.Entities;
using Motore.Library.Utils.Json;
using Motore.Performance.Web.Controllers;
using Motore.Performance.Web.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace Motore.Performance.Web.Tests.Controllers.CustomerControllerTests
{
    [TestFixture]
    public class When_submitting_a_customer_email
    {
        private AlphaProgram _program;
        private string _emailAddressSubmitted;
        private JsonResult _result;
        private GenericResponse _genericResponse;
        private JsonResponse _responseData;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            // Arrange
             _emailAddressSubmitted = "iwanttopayforyourservice@reporting.com";
            _genericResponse = new GenericResponse(true,"some message");

            _program = MockRepository.GenerateStub<AlphaProgram>();
            _program.Stub(p => p.SaveInterestedCustomer(Arg<string>.Is.Anything)).Return(_genericResponse);

            var controller = new CustomerController(_program);

            // Act
            _result = controller.SubmitCustomerEmail(new SubmitCustomerEmailModel {CustomerEmail = _emailAddressSubmitted});
            _responseData = _result.Data as JsonResponse;
        }

        [Test]
        public void Then_the_email_is_submitted_to_the_alpha_program()
        {
            _program.AssertWasCalled(p=>p.SaveInterestedCustomer(_emailAddressSubmitted));
        }

        [Test]
        public void Then_the_response_from_submitting_is_a_json_response()
        {
            var data = _result.Data as JsonResponse;
            Assert.That(data,Is.Not.Null);
        }

        [Test]
        public void Then_the_result_shows_the_message()
        {
            
            Assert.That(_responseData.message,Is.EqualTo(_genericResponse.Message));
        }

        [Test]
        public void Then_the_result_shows_if_it_was_successful()
        {
            Assert.That(_responseData.success, Is.EqualTo(_genericResponse.Success));
        }
    }
}

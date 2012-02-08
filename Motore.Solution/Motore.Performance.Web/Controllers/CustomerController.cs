using System.Web.Mvc;
using Directus.SimpleDb.Providers;
using Motore.Library.Models;
using Motore.Library.Programs;
using Motore.Library.Utils.Json;
using Motore.Performance.Web.Models;

namespace Motore.Performance.Web.Controllers
{
    /// <summary>
    /// Controller for customer data
    /// </summary>
    public class CustomerController : Controller
    {
        private readonly AlphaProgram _program;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerController():this(new AlphaProgram(new SimpleDBProvider<Customer, string>(Properties.Settings.Default.AwsAccessKey,Properties.Settings.Default.AwsSecretKey)))
        {
            
        }

        /// <summary>
        /// Constructor with dependencies
        /// </summary>
        /// <param name="program"></param>
        public CustomerController(AlphaProgram program)
        {
            _program = program;
        }

        /// <summary>
        /// Submits a potential customer email address
        /// </summary>
        /// <param name="model">View Model with the email address</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitCustomerEmail(SubmitCustomerEmailModel model)
        {
            var response = _program.SaveInterestedCustomer(model.CustomerEmail);

            return Json(new JsonResponse(response));
        }

    }
}

using System.Web.Mvc;
using Motore.Library.Programs;
using Motore.Library.Utils.Json;
using Motore.Performance.Web.Models;

namespace Motore.Performance.Web.Controllers
{
    public class CustomerController : Controller
    {
        [HttpPost]
        public ActionResult SubmitCustomerEmail(SubmitCustomerEmailModel model)
        {

            var program = new AlphaProgram();
            var response = program.SaveInterestedCustomer(model.CustomerEmail);
            return Json(new JsonResponse(response));
        }

    }
}

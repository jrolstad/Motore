using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Factories;
using Motore.Library.Models.ReportWizard;

namespace Motore.Performance.Web.Controllers
{
    public class ReportWizardController : Controller
    {
        //
        // GET: /ReportWizard/

        public ActionResult Index()
        {
            // redirect to the first page in the ReportWizard process
            return RedirectToAction("Step", new { id = 1 });
        }

        [HttpGet]
        public ActionResult Step(int id)
        {
            var model = new Step(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult WizardStep(string token, int stepNumber)
        {
            var stepFactory = new ReportWizardStepFactory();
            var model = stepFactory.GetStep(stepNumber, token);
            return View("~/Views/ReportWizard/Step.cshtml", model);
        }

        [HttpPost]
        public ActionResult SelectCustodian(Custodian model)
        {
            var custodianName = model.CustodianName;
            // do something meaningful with the custodian
            return RedirectToRoute("ReportWizardStep", new {token = model.Token, stepNumber = 2});
        }

        [HttpPost]
        public ActionResult UploadHoldingsFile(UploadFile model)
        {
            var postedFile = model.PostedFile;
            var token = model.Token;

            // do something meaningful with the holdings file

            return RedirectToRoute("ReportWizardStep", new { token = model.Token, stepNumber = 3 });

        }

        [HttpPost]
        public ActionResult UploadTransactionFile(UploadFile model)
        {
            var postedFile = model.PostedFile;
            var token = model.Token;

            // do something meaningful with the transaction file

            return RedirectToRoute("ReportWizardStep", new { token = model.Token, stepNumber = 4 });

        }

        [HttpPost]
        public ActionResult SetAssumptions(Assumptions model)
        {
            // do something meaningful with the assumptions values for tax rates
            return RedirectToRoute("ReportWizardStep", new {token = model.Token, stepNumber = 5});
        }
    }
}

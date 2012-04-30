using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Models.Portfolio;
using Motore.Utils.Logging;
using Motore.Library.Models.Logging;

namespace Motore.Performance.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult WriteLog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriteLog(string message)
        {
            Log.WriteDebug(message);
            var model = new WriteLogResponse
                            {
                                Message = "Your log message was written",
                            };
            return View("~/Views/Test/WriteLog.cshtml", model);
        }

    }
}

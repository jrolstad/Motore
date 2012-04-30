using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;

namespace Motore.Admin.Web.Controllers
{
    public class SimpleDbController : Controller
    {
        private SimpleDbClient _simpleDbClient = null;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Domains()
        {
            var model = this.SimpleDbClient.ListDomains();
            return View(model);
        }

        #region Protected Properties

        protected internal virtual SimpleDbClient SimpleDbClient
        {
            get { return _simpleDbClient ?? (_simpleDbClient = AwsClientFactory.CreateSimpleDbClient()); }
        }

        #endregion

    }
}

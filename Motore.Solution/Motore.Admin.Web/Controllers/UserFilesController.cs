using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motore.Admin.Web.Controllers
{
    public class UserFilesController : Controller
    {
        //
        // GET: /UserFiles/

        public ActionResult Index()
        {
            var model = this.UserFilesProvider.GetAll();
            return View();
        }

        #region Protected Properties

        protected internal virtual UserFilesProvider UserFilesProvider
        {
            get { return _provider ?? (_provider = new UserFilesProvider()); }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Files;

namespace Motore.Admin.Web.Controllers
{
    public class UserFilesController : Controller
    {
        private UserFilesProvider _provider = null;
        //
        // GET: /UserFiles/

        public ActionResult Index()
        {
            var model = this.UserFilesProvider.GetAll();
            return View(model);
        }

        public ActionResult Details(string id)
        {
            var model = this.UserFilesProvider.Get(id);
            return View(model);
        }
    
        #region Protected Properties

        protected internal virtual UserFilesProvider UserFilesProvider
        {
            get { return _provider ?? (_provider = new UserFilesProvider()); }
        }

        #endregion

    }
}

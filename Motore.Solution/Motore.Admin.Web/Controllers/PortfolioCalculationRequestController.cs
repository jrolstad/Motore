using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Portfolios.Requests;

namespace Motore.Admin.Web.Controllers
{
    public class PortfolioCalculationRequestController : Controller
    {
        private PortfolioCalculationRequestProvider _provider = null;
        //
        // GET: /PortfolioRequest/

        public ActionResult Index()
        {
            var model = this.PortfolioCalculationRequestProvider.GetMostRecent100Requests();
            return View(model);
        }

        #region Protected Properties

        protected internal virtual PortfolioCalculationRequestProvider PortfolioCalculationRequestProvider
        {
            get { return _provider ?? (_provider = new PortfolioCalculationRequestProvider()); }
        }

        #endregion

    }
}

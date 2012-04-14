using System.Web.Mvc;
using Motore.Library.Factories;
using Motore.Library.MarketData;
using Motore.Library.Models;

namespace Motore.Performance.Web.Controllers
{
    public class MarketDataController : Controller
    {
        private MarketDataRequestQueue _marketDataRequestQueue = null;
        private QueueFactory _queueFactory = null;

        [HttpGet]
        public ActionResult MakeRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitMarketDataRequest(MarketDataRequest request)
        {
            var queue = this.MarketDataRequestQueue;
            var response = queue.Add(request);
            var model = ModelFactory.Convert(response);
            return View("~/Views/MarketData/MakeRequest.cshtml", model);
        }

        #region Protected Properties

        protected internal virtual QueueFactory QueueFactory
        {
            get { return _queueFactory ?? (_queueFactory = new QueueFactory()); }
        }

        protected internal virtual MarketDataRequestQueue MarketDataRequestQueue
        {
            get { return _marketDataRequestQueue ?? (_marketDataRequestQueue = this.QueueFactory.GetMarketDataRequestQueue()); }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motore.Library.Configuration;
using Motore.Library.Exceptions;
using Motore.Library.Models.Portfolio;
using Motore.Library.Portfolios.Requests;

namespace Motore.Performance.Web.Controllers
{
    public class PortfolioCalculationRequestController : Controller
    {
        private PortfolioCalculationRequestProvider _provider = null;

        [HttpGet]
        public ActionResult Status(string id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubmitRequest(string requestId)
        {
            var actualRequestId = (String.IsNullOrWhiteSpace(requestId) ? this.Provider.CreateNewRequestId() : requestId);
            var model = new PortfolioCalculationRequestModel
                            {
                                RequestId = actualRequestId,
                            };
            
            return View("~/Views/PortfolioCalculationRequest/Submit.cshtml", model);
        }

        [HttpPost]
        public ActionResult SubmitRequest(PortfolioCalculationRequestModel model)
        {
            try
            {
                this.ValidateNewRequest(model);
                return View("~/Views/PortfolioCalculationRequest/ResponseOk.cshtml", model);
            }
            catch (PortfolioCalculationRequestValidationException pexc)
            {
                this.Provider.LogRequestError(model.RequestId, pexc.Message);
                var exceptionModel = new PortfolioCalculationRequestExceptionModel
                                         {
                                             RequestId = model.RequestId,
                                             ValidationException = pexc
                                         };
                return View("~/Views/PortfolioCalculationRequest/RequestException.cshtml", exceptionModel);
            }
        }

        #region Protected Methods

        protected internal virtual void ValidateNewRequest(PortfolioCalculationRequestModel model)
        {
            PortfolioCalculationRequestValidationException exc = null;

            if (model.PortfolioFile == null)
            {
                (exc ?? (exc = new PortfolioCalculationRequestValidationException()))
                    .Add(new Exception("You must provide a portfolio file."));
            }
            else
            {
                if (String.IsNullOrWhiteSpace(model.PortfolioFile.FileName))
                {
                    (exc ?? (exc = new PortfolioCalculationRequestValidationException()))
                        .Add(new Exception("You must provide a portfolio file."));
                }
                if (model.PortfolioFile.ContentLength > Config.MaximumPortfolioFileUploadSizeInBytes)
                {
                    (exc ?? (exc = new PortfolioCalculationRequestValidationException()))
                        .Add(new PortfolioFileSizeException(model.PortfolioFile.FileName, model.PortfolioFile.ContentLength, Config.MaximumPortfolioFileUploadSizeInBytes));
                }
            }

            if (exc != null)
            {
                throw exc;
            }
        }

        #endregion

        #region Protected Properties

        protected internal virtual PortfolioCalculationRequestProvider Provider
        {
            get { return (_provider ?? (_provider = new PortfolioCalculationRequestProvider())); }
        }

        #endregion
    }
}

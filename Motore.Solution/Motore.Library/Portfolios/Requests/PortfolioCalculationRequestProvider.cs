using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Entities;
using Motore.Library.Models.Portfolio;
using Motore.Utils.Assertions;
using Motore.Utils.Logging;

namespace Motore.Library.Portfolios.Requests
{
    public class PortfolioCalculationRequestProvider
    {
        private SimpleDbClient _simpleDbClient = null;

        public virtual IEnumerable<PortfolioCalculationRequestViewModel> GetMostRecent100Requests()
        {
            string nextToken = null;
            var requests = this.SimpleDbClient.Get<PortfolioCalculationRequest>(100, ref nextToken);
            return this.Convert(requests);
        }

        public virtual PortfolioCalculationRequestInputModel SubmitRequest(PortfolioCalculationRequestInputModel input)
        {
            // save the request id
            Assert.Fail(() => (input != null), "The PortfolioCalculationRequestModel parameter is null");
            Assert.Fail(()=>(String.IsNullOrWhiteSpace(input.RequestId)), "The RequestId property of the PortfolioCalculationRequestModel is null or whitespace");

            var model = new PortfolioCalculationRequestInputModel();
            this.SaveInitialRequest(input);
            throw new NotImplementedException();
        }

        public virtual void LogRequestError(string requestId, string error)
        {
            var message = String.Format("Portfolio Calculation Request Error [ID {0}]: {1}", requestId, error);
            Log.LogException(message);
        }

        public virtual string CreateNewRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        #region Protected Methods

        protected internal virtual IEnumerable<PortfolioCalculationRequestViewModel> Convert(List<PortfolioCalculationRequest> requests)
        {
            IEnumerable<PortfolioCalculationRequestViewModel> models = new List<PortfolioCalculationRequestViewModel>();
            if ((requests != null)
                && (requests.Count > 0))
            {
                models = requests.Select(Convert);
            }
            return models;
        }

        protected internal virtual PortfolioCalculationRequestViewModel Convert(PortfolioCalculationRequest request)
        {
            var model = new PortfolioCalculationRequestViewModel
                            {
                                RequestId = request.RequestId,
                                RequestDate = request.RequestDate,
                                ClientIp = request.ClientIp,
                                CreatedBy = request.CreatedBy,
                                CreateTimestamp = request.CreateTimestamp,
                                ModifiedBy = request.ModifiedBy,
                                ModifyTimestamp = request.ModifyTimestamp,
                                PortfolioFileInfo = request.PortfolioFileInfo,
                                Status = request.Status,
                            };
            return model;
        }

        protected internal virtual PortfolioCalculationRequest Convert(PortfolioCalculationRequestInputModel model)
        {
            var request = new PortfolioCalculationRequest
                              {
                                  ClientIp = model.ClientIp,
                                  CreatedBy = model.CreatedBy,
                                  CreateTimestamp = model.CreateTimestamp,
                                  ModifiedBy = model.ModifiedBy,
                                  ModifyTimestamp = model.ModifyTimestamp,
                                  Origin = model.Origin,
                                  RequestDate = model.RequestDate,
                                  RequestId = model.RequestId,
                                  Status = PortfolioCalculationRequestStatus.Pending,

                              };

            return request;
        }
        protected internal virtual void SaveInitialRequest(PortfolioCalculationRequestInputModel model)
        {
            var request = this.Convert(model);
            this.SimpleDbClient.SaveEntity<PortfolioCalculationRequest>(request);
        }

        #endregion

        #region Protected Properties

        protected internal virtual SimpleDbClient SimpleDbClient
        {
            get { return _simpleDbClient ?? (_simpleDbClient = AwsClientFactory.CreateSimpleDbClient()); }
            set { _simpleDbClient = value; }
        }

        #endregion

    }
}

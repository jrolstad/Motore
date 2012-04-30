using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Models.Portfolio;
using Motore.Utils.Assertions;
using Motore.Utils.Logging;

namespace Motore.Library.Portfolios.Requests
{
    public class PortfolioCalculationRequestProvider
    {
        private SimpleDbClient _simpleDbClient = null;

        public virtual PortfolioCalculationRequestModel SubmitRequest(PortfolioCalculationRequestModel input)
        {
            // save the request id
            Assert.Fail(() => (input != null), "The PortfolioCalculationRequestModel parameter is null");
            Assert.Fail(()=>(String.IsNullOrWhiteSpace(input.RequestId)), "The RequestId property of the PortfolioCalculationRequestModel is null or whitespace");

            var model = new PortfolioCalculationRequestModel();
            this.SaveRequestId(input.RequestId);
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

        protected internal virtual void SaveRequestId(string requestId)
        {
            throw new NotImplementedException();
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

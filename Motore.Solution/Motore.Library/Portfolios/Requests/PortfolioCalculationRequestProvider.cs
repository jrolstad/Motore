using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Logging;

namespace Motore.Library.Portfolios.Requests
{
    public class PortfolioCalculationRequestProvider
    {
        public virtual void LogRequestError(string requestId, string error)
        {
            var message = String.Format("Portfolio Calculation Request Error [ID {0}]: {1}", requestId, error);
            Log.LogException(message);
        }

        public virtual string CreateNewRequestId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestSubmitModel
    {
        #region Member Variables

        private string _requestId = null;
        private PortfolioCalculationRequestStatus _status = PortfolioCalculationRequestStatus.New;
        #endregion

        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
        
        public virtual PortfolioCalculationRequestStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        
    }
}

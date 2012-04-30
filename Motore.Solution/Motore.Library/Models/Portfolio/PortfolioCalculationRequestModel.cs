using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();

        #endregion

        public virtual System.Web.HttpPostedFileBase PortfolioFile { get; set; }
        public virtual string RequestId
        {
            get { return _requestId; } 
            set { _requestId = value; }
        }
    }
}

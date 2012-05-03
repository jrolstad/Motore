using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestInputModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();

        #endregion

        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
        
        public virtual DateTime RequestDate { get; set; }
        public virtual string ClientIp { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual long CreateTimestamp { get; set; }
        public virtual long ModifyTimestamp { get; set; }

        public virtual System.Web.HttpPostedFileBase PortfolioFile { get; set; }
        
    }
}

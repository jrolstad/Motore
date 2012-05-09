using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestViewModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();
        private DateTime _requestDate = DateTime.UtcNow;

        #endregion

        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
        
        public virtual DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = value; }
        }

        public virtual string ClientIp { get; set; }
        public virtual PortfolioCalculationRequestStatus Status { get; set; }
        
        // this seems very vague.  What am I trying to do here?  What's the client scenario that needs this data?
        public virtual string PortfolioFileInfo { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual long CreateTimestamp { get; set; }
        public virtual long ModifyTimestamp { get; set; }

        public virtual string RequestDateDisplayString
        {
            get { return this.RequestDate.ToUniversalTime().ToString("M/d/yy HH:mm"); }
        }
    }
}

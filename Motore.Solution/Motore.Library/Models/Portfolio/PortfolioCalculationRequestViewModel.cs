using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Utils.Dates;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestViewModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();
        private long _requestTimestamp = SystemTime.Now().ToTimestamp();

        #endregion

        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
        
        public virtual string RequestDateString
        {
            get
            {
                string result = "ERROR";
                if (this.RequestTimestamp > 0)
                {
                    result = DateUtils.FromTimestamp(this.RequestTimestamp).ToString("R");
                }
                return result;
            }
        }

        public virtual long RequestTimestamp
        {
            get { return _requestTimestamp; }
            set { _requestTimestamp = value; }
        }

        public virtual string ClientIp { get; set; }
        public virtual PortfolioCalculationRequestStatus Status { get; set; }
        
        // TODO
        // this seems very vague.  What am I trying to do here?  What's the client scenario that needs this data?
        public virtual string PortfolioFileInfo { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual long CreateTimestamp { get; set; }
        public virtual long ModifyTimestamp { get; set; }

    }
}

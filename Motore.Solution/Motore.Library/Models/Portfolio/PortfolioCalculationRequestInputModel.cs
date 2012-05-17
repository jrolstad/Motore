using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Motore.Utils.Dates;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestInputModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();
        private string _portfolioFileType = "unknown";
        private long _requestTimestamp = SystemTime.Now().ToTimestamp();

        #endregion

        public virtual Stream FileStream
        {
            get
            {
                Stream s = null;
                if (this.PortfolioFile != null)
                {
                    s = this.PortfolioFile.InputStream;
                }
                return s;
            }
        }
        public virtual string ClientFileName
        {
            get
            { 
                string result = null;
                if (this.PortfolioFile != null)
                {
                    result = this.PortfolioFile.FileName;
                }
                return result;
            }
        }

        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
        
        public virtual long RequestTimestamp
        {
            get { return _requestTimestamp; }
            set { _requestTimestamp = value; }
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

        public virtual string ClientIp { get; set; }
        
        /// <summary>
        /// IE schwab, fidelity, rbc, scottrade, etrade, etc.
        /// </summary>
        public virtual string PortfolioFileType
        {
            get { return _portfolioFileType; }
            set { _portfolioFileType = value; }
        }

        public virtual string OriginalFileName
        {
            get
            {
                var originalFileName = "UNKNOWN";
                if ((this.PortfolioFile != null)
                    && (!String.IsNullOrWhiteSpace(this.PortfolioFile.FileName)))
                {
                    originalFileName = this.PortfolioFile.FileName;
                }
                return originalFileName;
            }
        }

        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual long CreateTimestamp { get; set; }
        public virtual long ModifyTimestamp { get; set; }
        public virtual string Origin { get; set; }

        public virtual System.Web.HttpPostedFileBase PortfolioFile { get; set; }
        
    }
}

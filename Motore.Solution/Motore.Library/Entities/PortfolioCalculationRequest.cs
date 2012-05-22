using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws.Attributes;
using Motore.Library.Aws.SimpleDb;
using Motore.Utils.Dates;

namespace Motore.Library.Entities
{
    public enum PortfolioCalculationRequestStatus
    {
        New = 1,
        ProcessingInput = 3,
        ValidatingData = 5,
        Calculating = 8,
        ReportGeneration = 12,
        Notifying = 15,
        Completed = 20,
        Error = 666
    }

    [SimpleDbDomain(Domain="PortfolioCalculationRequest")]
    public class PortfolioCalculationRequest : ISimpleDbEntity
    {
        private long _requestTimestamp = SystemTime.Now().ToTimestamp();
        private string _requestId = Guid.NewGuid().ToString();
        private string _origin = null;

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "RequestId", IsPrimaryKey = true)]
        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        [SimpleDbColumn(Name="Origin", Multiplicity=ColumnMultiplicity.Single)]
        public virtual string Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "RequestTimestamp")]
        protected internal virtual string RequestTimestampString
        {
            get { return this.RequestTimestamp.ToString(); }
            set
            {
                long timestamp;
                if (!long.TryParse(value, out timestamp))
                {
                    timestamp = DateUtils.ToTimestamp(SystemTime.Now());
                }
                this.RequestTimestamp = timestamp;
            }
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
                var result = "ERROR";
                if (this.RequestTimestamp > 0)
                {
                    result = DateUtils.FromTimestamp(this.RequestTimestamp).ToString("R");
                }
                return result;
            }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ClientIp")]
        public string ClientIp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "PortfolioFileInfo")]
        public virtual string PortfolioFileInfo { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Status")]
        protected internal virtual string StatusString
        {
            get { return this.Status.ToString(); }
            set
            {
                this.Status =
                    (PortfolioCalculationRequestStatus)
                    Enum.Parse(typeof (PortfolioCalculationRequestStatus), value, true);
            }
        }

        public virtual PortfolioCalculationRequestStatus Status { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreateTimestamp")]
        protected internal virtual string CreateTimestampString
        {
            get { return this.CreateTimestamp.ToString(); }
            set
            {
                long timestamp;
                if (!long.TryParse(value, out timestamp))
                {
                    timestamp = DateUtils.ToTimestamp(SystemTime.Now());
                }
                this.CreateTimestamp = timestamp;

            }
        }

        public virtual long CreateTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifyTimestamp")]
        protected internal virtual string ModifyTimestampString
        {
            get { return this.ModifyTimestamp.ToString(); }
            set
            {
                long timestamp;
                if (!long.TryParse(value, out timestamp))
                {
                    timestamp = DateUtils.ToTimestamp(SystemTime.Now());
                }
                this.ModifyTimestamp = timestamp;

            }
        }
        public virtual long ModifyTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreatedBy")]
        public virtual string CreatedBy { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifiedBy")]
        public virtual string ModifiedBy { get; set; }
    }
}

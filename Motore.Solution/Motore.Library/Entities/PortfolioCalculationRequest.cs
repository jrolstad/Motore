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
        Pending = 1,
        Started = 2,
        Completed = 10,
        Error = 666
    }

    [SimpleDbDomain(Domain="PortfolioCalculationRequest")]
    public class PortfolioCalculationRequest : ISimpleDbEntity
    {
        private DateTime _requestDate = SystemTime.Now();
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
            set
            {
                long timestamp;
                if (long.TryParse(value, out timestamp))
                {
                    this.RequestDate = DateUtils.FromTimestamp(timestamp);
                }
            }
        }

        public virtual DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ClientIp")]
        public string ClientIp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Status")]
        protected internal virtual string StatusString
        {
            set
            {
                this.Status =
                    (PortfolioCalculationRequestStatus)
                    Enum.Parse(typeof (PortfolioCalculationRequestStatus), value, true);
            }
        }
        public PortfolioCalculationRequestStatus Status { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Errors")]
        public virtual string Errors { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreateTimestamp")]
        protected internal virtual string CreateTimestampString
        {
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

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "PortfolioFileInfo")]
        public virtual string PortfolioFileInfo { get; set; }

    }
}

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

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "RequestId", IsPrimaryKey = true)]
        public virtual string RequestId { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "RequestDate")]
        public virtual DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = value; }
        }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ClientIp")]
        public string ClientIp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Status")]
        public PortfolioCalculationRequestStatus Status { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "Errors")]
        public string Errors { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreateTimestamp")]
        public long CreateTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifyTimestamp")]
        public long ModifyTimestamp { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [SimpleDbColumn(Multiplicity = ColumnMultiplicity.Single, Name = "ModifiedBy")]
        public string ModifiedBy { get; set; }

    }
}

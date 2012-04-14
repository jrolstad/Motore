using System;
using System.Runtime.Serialization;
using Motore.Library.Utils;

namespace Motore.Library.MarketData
{
    [DataContract]
    public class MarketDataRequest
    {
        private DateTime? _endDate;
        private string _requestId;
        private DateTime? _startDate;

        public MarketDataRequest()
        {
            _requestId = DateUtils.ToTimestamp(DateTime.Now).ToString();
            _endDate = DateTime.Now;
            _startDate = _endDate.Value.AddMonths(-1);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "Id", Order = 1)]
        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        [DataMember(IsRequired = true, Name = "identifier", EmitDefaultValue = false, Order = 2)]
        public virtual string Identifier { get; set; }

        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [DataMember(IsRequired = false, Name = "start", EmitDefaultValue = false, Order = 3)]
        public virtual string StartDateTimestamp
        {
            get
            {
                string val = null;
                if (StartDate.HasValue)
                {
                    long ts = DateUtils.ToTimestamp(StartDate.Value);
                    val = ts.ToString();
                }
                return val;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    StartDate = DateUtils.FromTimestamp(value);
                }
                else
                {
                    StartDate = null;
                }
            }
        }


        [DataMember(IsRequired = false, Name = "end", EmitDefaultValue = false, Order = 4)]
        public virtual string EndDateTimestamp
        {
            get
            {
                string val = null;
                if (EndDate.HasValue)
                {
                    long ts = DateUtils.ToTimestamp(EndDate.Value);
                    val = ts.ToString();
                }
                return val;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    EndDate = DateUtils.FromTimestamp(value);
                }
                else
                {
                    EndDate = null;
                }
            }
        }
    }
}
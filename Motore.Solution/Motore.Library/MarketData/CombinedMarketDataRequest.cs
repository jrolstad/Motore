using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Motore.Library.Utils;

namespace Motore.Library.MarketData
{
    [DataContract]
    public class CombinedMarketDataRequest
    {
        private string _requestId;
        private List<InstrumentMarketDataRequest> _instrumentRequests = null;

        public CombinedMarketDataRequest()
        {
            _requestId = DateUtils.ToTimestamp(DateTime.Now).ToString();
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "id", Order = 1)]
        public virtual string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        [DataMember(IsRequired = false, Name = "requests", EmitDefaultValue = false, Order = 2)]
        public virtual List<InstrumentMarketDataRequest> InstrumentRequests
        {
            get { return _instrumentRequests; }
            set { _instrumentRequests = value; }
        }
    }
}
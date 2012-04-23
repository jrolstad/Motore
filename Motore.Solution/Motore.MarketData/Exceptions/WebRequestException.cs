using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.MarketData.Exceptions
{
    public class WebRequestException : Exception
    {
        private InstrumentMarketDataRequest _request = null;
        private string _url = null;

        public WebRequestException(InstrumentMarketDataRequest request, string url, Exception innerException) : base("", innerException)
        {
            _request = request;
            _url = url;
            
        }

        public override string Message
        {
            get
            {
                var msg =
                    "Error obtaining data at the url '{0}'.  Identifier: '{1}'.  Start Date: '{2}'.  EndDate: '{3}'.";
                msg = String.Format(msg, _url, _request.Identifier, _request.StartDate, _request.EndDate);
                return msg;
            }
        }

    }
}

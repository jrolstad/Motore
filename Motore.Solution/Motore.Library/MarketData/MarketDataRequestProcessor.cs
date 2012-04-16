using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.MarketData
{
    public class MarketDataRequestProcessor
    {
        private MarketDataRequestQueue _queue = null;

        public void Process()
        {
            var messagesToProcess = this.MarketDataRequestQueue.GetRequests();
            if (messagesToProcess != null)
            {
                messagesToProcess.ForEach(ProcessRequest);
            }
        }

        #region Protected Properties

        protected internal virtual IMarketDataProvider GetMarketDataProvider(InstrumentMarketDataRequest request)
        {
            throw new NotImplementedException();
        }

        protected internal virtual void ProcessRequest(InstrumentMarketDataRequest request)
        {
            throw new NotImplementedException();
        }

        protected internal virtual MarketDataRequestQueue MarketDataRequestQueue
        {
            get { return _queue ?? (_queue = new MarketDataRequestQueue()); }
        }

        #endregion

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.MarketData;

namespace Motore.Library.Factories
{
    public class QueueFactory
    {
        private MarketDataRequestQueue _marketDataRequestQueue = null;

        #region Public Methods

        public virtual MarketDataRequestQueue GetMarketDataRequestQueue()
        {
            var queue = this.MarketDataRequestQueue;
            return queue;
        }

        #endregion

        #region Protected Properties

        protected internal virtual MarketDataRequestQueue MarketDataRequestQueue
        {
            get { return _marketDataRequestQueue ?? (_marketDataRequestQueue = new MarketDataRequestQueue()); }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketData.Yahoo;
using Motore.Library.MarketData;
using Motore.MarketData;

namespace Motore.Library.Factories
{
    public class ProviderFactory
    {
        private IMarketDataProvider _defaultMarketDataProvider = null;

        public virtual IMarketDataProvider GetMarketDataProvider()
        {
            return this.DefaultMarketDataProvider;
        }

        public virtual IMarketDataProvider GetMarketDataProvider(InstrumentMarketDataRequest request)
        {
            throw new NotImplementedException();
        }

        #region Protected Properties

        protected internal virtual IMarketDataProvider DefaultMarketDataProvider
        {
            get { return _defaultMarketDataProvider ?? (_defaultMarketDataProvider = new YahooMarketDataProvider()); }
            set { _defaultMarketDataProvider = value; }
        }

        #endregion

    }
}

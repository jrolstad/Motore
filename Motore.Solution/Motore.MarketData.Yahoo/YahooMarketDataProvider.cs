using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.MarketData.Exceptions;
using Motore.Utils.Exceptions.Web;
using Motore.Utils.Logging;

namespace Motore.MarketData.Yahoo
{
    public class YahooMarketDataProvider : IMarketDataProvider
    {
        private IHistoricalStockDataProvider _historicalStockDataProvider = null;

        public virtual IEnumerable<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request)
        {
            try
            {
                return this.HistoricalStockDataProvider.GetMarketData(request);
            }
            catch (NotFoundException nfe)
            {
                throw new WebRequestException(request, nfe.Url, nfe);
            }
            catch (Exception exc)
            {
                Log.LogException(exc);
                throw;
            }
        }

        #region Protected Properties

        protected internal virtual IHistoricalStockDataProvider HistoricalStockDataProvider
        {
            get
            {
                if (_historicalStockDataProvider == null)
                {
                    _historicalStockDataProvider = new HistoricalStockDataCsvProvider();
                }
                return _historicalStockDataProvider;
            }
        }
    
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.MarketData;
using Motore.Utils.Assertions;
using Motore.Utils.Web;

namespace Motore.MarketData.Yahoo
{
    public class HistoricalStockDataCsvProvider : IHistoricalStockDataProvider
    {
        private UrlBuilder _urlBuilder = null;
        private DateFactory _dateFactory = null;
        private HttpClient _httpClient = null;

        public virtual IEnumerable<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request)
        {
            Assert.Fail(()=> (request != null), "Request is null!");
            Assert.Fail(()=>(!String.IsNullOrWhiteSpace(request.Identifier)), "Identifier is not specified!");
            Assert.Fail(() => (request.StartDate.HasValue), "StartDate is not specified!");
            Assert.Fail(()=> (request.EndDate.HasValue), "EndDate is not specified!");

            var identifier = request.Identifier;
            var startDate = request.StartDate.GetValueOrDefault();
            var endDate = request.EndDate.GetValueOrDefault();

            var convertedStart = this.DateFactory.ConvertDate(startDate);
            var convertedEnd = this.DateFactory.ConvertDate(endDate);

            var url = this.UrlBuilder.BuildCsvUrl(identifier, convertedStart, convertedEnd);

            throw new NotImplementedException();

        }

        #region Protected Properties

        protected internal virtual UrlBuilder UrlBuilder
        {
            get { return _urlBuilder ?? (_urlBuilder = new UrlBuilder()); }
        }

        protected internal virtual DateFactory DateFactory
        {
            get { return _dateFactory ?? (_dateFactory = new DateFactory()); }
        }

        protected internal virtual HttpClient HttpClient
        {
            get { return _httpClient ?? (_httpClient = new HttpClient()); }
        }

        #endregion

    }
}

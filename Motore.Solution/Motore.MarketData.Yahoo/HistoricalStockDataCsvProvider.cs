using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.MarketData;
using Motore.Utils.Assertions;
using Motore.Utils.Dates;
using Motore.Utils.Text;
using Motore.Utils.Web;

namespace Motore.MarketData.Yahoo
{
    public class HistoricalStockDataCsvProvider : IHistoricalStockDataProvider
    {
        private UrlBuilder _urlBuilder = null;
        private DateFactory _dateFactory = null;
        private HttpClient _httpClient = null;
        private IMarketDataFactory _marketDataFactory = null;

        public virtual IEnumerable<DailyInstrumentMarketData> GetMarketData(InstrumentMarketDataRequest request)
        {
            Assert.Fail(()=> (request != null), "Request is null!");
            Assert.Fail(()=>(!String.IsNullOrWhiteSpace(request.Identifier)), "Identifier is not specified!");
            
            var identifier = request.Identifier;
            var startMdy = this.ConvertDate(request.StartDate);
            var endMdy = this.ConvertDate(request.EndDate);

            var results = this.GetResults(identifier, startMdy, endMdy);
            return results;
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

        protected internal virtual IMarketDataFactory MarketDataFactory
        {
            get { return _marketDataFactory ?? (_marketDataFactory = new YahooMarketDataFactory()); }
        }

        #endregion

        #region Protected Methods

        protected internal virtual IEnumerable<DailyInstrumentMarketData> GetResults(string identifier, YahooMdy start, YahooMdy end)
        {
            var url = this.UrlBuilder.BuildCsvUrl(identifier, start, end);
            var results = this.HttpClient.GetCsv(url);
            return this.ConvertCsvLines(identifier, results);
        }

        protected internal virtual IEnumerable<DailyInstrumentMarketData> ConvertCsvLines(string identifier, IEnumerable<string> csvLines)
        {
            var results = new List<DailyInstrumentMarketData>();
            if (csvLines != null)
            {
                foreach (var line in csvLines)
                {
                    if (this.IsDataRow(line))
                    {
                        var data = this.MarketDataFactory.CreateDailyInstrumentMarketData(identifier, line);
                        results.Add(data);
                    }
                }
            }

            return results;
        }

        protected internal virtual bool IsDataRow(string input)
        {
            return (input ?? "").BeginsWithNumber();
        }

        protected internal virtual YahooMdy ConvertDate(DateTime? date)
        {
            Assert.Fail(()=>(date.HasValue), "The date passed in to ConvertDate() must have a value");
            var returnValue = date.GetValueOrDefault();
            var mdy = this.DateFactory.ConvertDate(returnValue);
            return mdy;
        }

        #endregion

    }
}

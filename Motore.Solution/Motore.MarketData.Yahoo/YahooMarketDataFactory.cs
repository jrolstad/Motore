using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Text;
using Motore.Utils.Assertions;

namespace Motore.MarketData.Yahoo
{
    public class YahooMarketDataFactory : IMarketDataFactory
    {
        public DailyInstrumentMarketData CreateDailyInstrumentMarketData(string input)
        {
            throw new NotImplementedException("The method CreateDailyInstrumentMarketData(string input) is not implemented on the YahooMarketDataFactory class.  Use the two-parameter call.");
        }

        public DailyInstrumentMarketData CreateDailyInstrumentMarketData(string identifier, string input)
        {
            // simplest thing that could possibly work
            var elements = input.Split(new char[1] {','});
            var date = elements[0];
            var open = elements[1];
            var high = elements[2];
            var low = elements[3];
            var close = elements[4];
            var volume = elements[5];
            var adjustedClose = elements[6];

            var data = new DailyInstrumentMarketData
                           {
                               Date = this.ConvertDate(date),
                               Identifier = identifier,
                               ClosingPrice = this.ConvertDecimal(close),
                               AdjustedClosingPrice = this.ConvertDecimal(adjustedClose)
                           };

            return data;

        }

        #region Protected Methods

        protected internal virtual DateTime ConvertDate(string input)
        {
            const string expectedPattern = @"^[\d]{4}-[\d]{2}-[\d]{2}$";
            Assert.Fail(() => (Regex.IsMatch(input, expectedPattern)),
                        String.Format("input date '{0}': unexpected format.", input));

            return DateTime.Parse(input);
        }

        protected internal virtual decimal ConvertDecimal(string input)
        {
            return Decimal.Parse(input);
        }

        #endregion


    }
}

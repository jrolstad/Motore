using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Utils.Dates;

namespace Motore.MarketData.Yahoo
{
    public class UrlBuilder
    {
        private const string _formatString = "http://ichart.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d&ignore=.csv";

        public virtual string BuildCsvUrl(string identifier, Mdy start, Mdy end)
        {
            var sd = start.Day;
            var sm = start.Month;
            var sy = start.Year;
            var ed = end.Day;
            var em = end.Month;
            var ey = end.Year;

            return String.Format(_formatString, identifier, sm, sd, sy, em, ed, ey);
        }
    }
}

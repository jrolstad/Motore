using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Utils.Assertions;

namespace Motore.Library.Portfolios.Parsers
{
    public class FidelityFileParser : PortfolioFileParserBase, IPortfolioFileParser
    {
        public virtual Portfolio Parse(UserFile userFile)
        {
            var portfolio = new Portfolio();
            var holdings = new List<Holding>();
            var lines = this.FileProvider.GetLinesFromFile(userFile.FileSystemType, userFile.Location);
            foreach (var line in lines)
            {
                var holding = this.ParseLine(line);
                holdings.Add(holding);
            }
            portfolio.Holdings = holdings;
            return portfolio;
        }

        protected internal virtual Holding ParseLine(string line)
        {
            if (this.IsPortfolioLine(line))
            {
                var elements = this.CreateElementsFromLine(line);
            }
            throw new NotImplementedException();
        }

        protected internal virtual IEnumerable<string> CreateElementsFromLine(string line)
        {
            var elements = (line ?? "").Split(new char[1] {','});
            Assert.Fail(() => (elements.Length == 21),
                        String.Format(
                            "A Fidelity portfolio extract line should contain exactly 21 elements.  This line '{0}' has only {1} elements.",
                            line, elements.Length));
            return elements;
        }

        protected internal virtual bool IsPortfolioLine(string line)
        {
            var isPortfolioLine = line.StartsWith("Account Name", true, CultureInfo.CurrentCulture);
            return isPortfolioLine;
        }
    }
}

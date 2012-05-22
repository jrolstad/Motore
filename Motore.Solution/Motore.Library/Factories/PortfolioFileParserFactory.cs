using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Library.Portfolios;
using Motore.Library.Portfolios.Parsers;

namespace Motore.Library.Factories
{
    public class PortfolioFileParserFactory
    {
        public virtual IPortfolioFileParser Create(UserFile userFile)
        {
            IPortfolioFileParser parser;
            var custodian = userFile.Custodian;
            switch (custodian)
            {
                case Custodian.Fidelity:
                    {
                        parser = new FidelityFileParser();
                        break;
                    }
                default:
                    {
                        var msg = String.Format("No file parser has been defined for the custodian '{0}'.", custodian);
                        throw new Exception(msg);
                    }
            }

            return parser;
        }
    }
}

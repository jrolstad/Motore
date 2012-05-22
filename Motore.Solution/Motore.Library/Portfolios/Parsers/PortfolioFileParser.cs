using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;
using Motore.Library.Factories;
using Motore.Library.Providers;

namespace Motore.Library.Portfolios
{
    public abstract class PortfolioFileParserBase
    {
        private FileProvider _fileProvider = null;

        #region Protected Properties

        protected internal virtual FileProvider FileProvider
        {
            get { return _fileProvider ?? (_fileProvider = new FileProvider()); }
        }

        #endregion

    }
}

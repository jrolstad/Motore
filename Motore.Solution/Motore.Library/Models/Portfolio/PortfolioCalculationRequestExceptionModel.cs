using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Exceptions;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestExceptionModel
    {
        #region Member Variables

        private string _requestId = Guid.NewGuid().ToString();

        #endregion

        #region Public Properties

        public PortfolioCalculationRequestValidationException ValidationException { get; set; }

        public virtual string RequestId
        {
            get { return _requestId; } 
            set { _requestId = value; }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Models.Portfolio
{
    public class PortfolioCalculationRequestNotifyDetailsModel
    {
        public virtual string RequestId { get; set; }
        public virtual string Email { get; set; }
    }
}

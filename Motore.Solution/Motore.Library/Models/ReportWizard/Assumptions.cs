using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Models.ReportWizard
{
    public class Assumptions : Step
    {
        protected double _marginalTaxRate = 0.35;
        protected double _longTermCapitalGainsRate = 0.15;
        protected double _shortTermCapitalGainsRate = 0.35;

        public virtual string MarginalTaxRate
        {
            get { return _marginalTaxRate.ToString("%"); }
            set { _marginalTaxRate = Double.Parse(value); }
        }

        public virtual string LongTermCapitalGainsRate
        {
            get { return _longTermCapitalGainsRate.ToString("%"); }
            set { _longTermCapitalGainsRate = Double.Parse(value); }
        }

        public virtual string ShortTermCapitalGainsRate
        {
            get { return _shortTermCapitalGainsRate.ToString("%"); }
            set { _shortTermCapitalGainsRate = Double.Parse(value); }
        }
    }
}

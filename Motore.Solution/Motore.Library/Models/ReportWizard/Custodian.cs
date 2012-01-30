using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Models.ReportWizard
{
    public class Custodian : Step
    {
        protected string _custodianName = null;

        public virtual string CustodianName
        {
            get { return _custodianName; }
            set { _custodianName = value; }
        }
    }
}

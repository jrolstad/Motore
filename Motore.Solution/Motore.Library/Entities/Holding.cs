using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Entities
{
    public class Holding
    {
        private string _currency = "USD";

        public virtual string Identifier { get; set; }
        public virtual Custodian Custodian { get; set; }
        public virtual string CustodianIdentifier { get; set; }
        public virtual long CostBasisTimestamp { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal Cost { get; set; }
        public virtual string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
    }
}

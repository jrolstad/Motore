using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.SimpleDb
{
    public class Domains : List<Domain>
    {
        public virtual string NextToken { get; set; }

        public virtual bool HasMoreResults
        {
            get { return !String.IsNullOrWhiteSpace(this.NextToken); }
        }
    }
}

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

        public virtual bool ContainsName(string name)
        {
            var contains = false;
            if (!String.IsNullOrWhiteSpace(name))
            {
                contains = this.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
            return contains;
        }
    }
}

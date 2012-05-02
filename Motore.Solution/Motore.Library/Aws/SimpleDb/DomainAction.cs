using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.SimpleDb
{
    public enum DomainActionType
    {
        NoAction = 0,
        Created = 1
    }

    public class DomainAction
    {
        public virtual string DomainName { get; set; }
        public virtual DomainActionType Action { get; set; }
    }
}

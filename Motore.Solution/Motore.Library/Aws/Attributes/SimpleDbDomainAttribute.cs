using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class)]
    public class SimpleDbDomainAttribute : Attribute
    {
        public virtual string Domain { get; set; }
    }
}

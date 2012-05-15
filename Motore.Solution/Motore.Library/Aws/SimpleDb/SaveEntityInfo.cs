using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.SimpleDb
{
    public class SaveEntityInfo
    {
        public virtual string PrimaryKey { get; set; }
        public Type EntityType { get; set; }
    }
}

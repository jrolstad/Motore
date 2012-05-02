using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Aws.Attributes
{
    public enum ColumnMultiplicity
    {
        Single = 1,
        Multiple = 2
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SimpleDbColumnAttribute : Attribute
    {
        private ColumnMultiplicity _multiplicity = ColumnMultiplicity.Single;

        public virtual ColumnMultiplicity Multiplicity
        {
            get { return _multiplicity; }
            set { _multiplicity = value; }
        }

        public virtual string Name { get; set; }
        public virtual bool IsPrimaryKey { get; set; }

    }
}

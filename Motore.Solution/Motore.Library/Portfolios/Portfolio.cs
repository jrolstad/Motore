using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Portfolios
{
    public class Portfolio
    {
        public virtual string Id { get; set; }
        public virtual List<Holding> Holdings { get; set; } 
    }
}

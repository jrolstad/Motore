using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Portfolios
{
    public interface IPortfolioFileParser
    {
        Portfolio Parse(UserFile file);
    }
}

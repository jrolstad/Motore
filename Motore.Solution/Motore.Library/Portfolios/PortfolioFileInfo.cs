using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Portfolios
{
    public class PortfolioFileInfo
    {
        public virtual FileSystemType FileSystemType { get; set; }
        public virtual string Uri { get; set; }
        public virtual string ClientFileName { get; set; }
        public virtual long UploadTimestamp { get; set; }
    }
}

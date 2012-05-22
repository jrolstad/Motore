using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Files
{
    public class RawFileModel
    {
        public virtual List<string> Lines { get; set; }
        public virtual string UserFileId { get; set; }
        public virtual FileSystemType FileSystemType { get; set; }
        public virtual string Location { get; set; }

        public virtual string FileSystemTypeString
        {
            get { return this.FileSystemType.ToString(); }
        }
    }
}

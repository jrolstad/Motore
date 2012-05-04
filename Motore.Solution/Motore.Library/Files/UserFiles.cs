using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Entities;

namespace Motore.Library.Files
{
    public class UserFiles : List<UserFile>
    {
        private string _nextToken = null;

        public virtual string NextToken
        {
            get { return _nextToken; }
            set { _nextToken = value; }
        }

        public virtual bool HasMoreFiles()
        {
            return !String.IsNullOrWhiteSpace(_nextToken);
        }
    }
}

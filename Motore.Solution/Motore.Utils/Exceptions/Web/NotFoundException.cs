using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Exceptions.Web
{
    public class NotFoundException : Exception
    {
        private string _url = null;

        public NotFoundException(string url)
        {
            _url = url;
        }

        public virtual string Url
        {
            get { return _url; }
        }

        public override string Message
        {
            get
            {
                var msg = String.Format("404 when navigating to '{0}'.", _url);
                return msg;
            }
        }

    }
}

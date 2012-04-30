using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Exceptions
{
    public class PortfolioFileSizeException : Exception
    {
        private string _clientFileName = null;
        private long _actualSize = 0;
        private long _maximumAllowedSize = 0;

        public PortfolioFileSizeException(string clientFileName, long actualSize, long maximumAllowedSize)
        {
            _clientFileName = clientFileName;
            _actualSize = actualSize;
            _maximumAllowedSize = maximumAllowedSize;
        }

        public override string Message
        {
            get
            {
                const string fmt =
                    "Your portfolio file '{0}' is too large.  The maximum allowed size is {1} bytes.  Your file is {2} bytes.";
                var msg = String.Format(fmt, _clientFileName, _maximumAllowedSize, _actualSize);
                return msg;

            }
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}

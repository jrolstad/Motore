using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Exceptions
{
    public class PortfolioCalculationRequestValidationException : Exception
    {
        private List<Exception> _exceptions = null;
 
        public virtual List<Exception> Exceptions
        {
            get { return _exceptions; }
            set { _exceptions = value; }
        }

        public virtual string Message
        {
            get { return this.CreateMessage(this.Exceptions, System.Environment.NewLine); }
        }

        public virtual string MessageAsHtml
        {
            get { return this.CreateMessage(this.Exceptions, "<p/>"); }
        }

        #region Public Methods

        public virtual void Add(Exception exception)
        {
            if (_exceptions == null)
            {
                _exceptions = new List<Exception>();
            }
            _exceptions.Add(exception);
        }

        #endregion

        #region Protected Methods

        protected internal virtual string CreateMessage(List<Exception> exceptions, string delimiter)
        {
            string combinedMsg = null;
            
            if ((exceptions != null)
                && (exceptions.Count > 0))
            {
                var sb = new StringBuilder();

                var count = exceptions.Count;
                for (var i = 0; i < count; i++)
                {
                    
                    var exc = this.Exceptions[i];
#if DEBUG
                    var itemMsg = exc.ToString();
#else
                    var itemMsg = exc.Message;
#endif
                    sb.Append(itemMsg);
                    if (i < (count - 1))
                    {
                        sb.Append(delimiter);
                    }
                }

                combinedMsg = sb.ToString();
            }

            return combinedMsg;
            
        }

        #endregion

    }
}

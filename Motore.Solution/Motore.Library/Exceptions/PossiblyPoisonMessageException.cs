using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Exceptions
{
    public class PossiblyPoisonMessageException : Exception
    {
        // TODO: how do we get a handle to the message so we can delete it?

        private string _message = null;
        private string _messageId = null;

        public PossiblyPoisonMessageException(string msgId, string exceptionMsg)
        {
            _messageId = msgId;
            _message = exceptionMsg;
        }

        public override string Message
        {
            get { return _message; }
        }

        public virtual string MessageId
        {
            get { return _messageId; }
        }
    }
}

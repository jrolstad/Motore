using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Utils.Entities
{
    public class GenericResponse
    {
        private bool _success = false;
        private string _message = "";
        private Exception _exception = null;

        public GenericResponse(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        public GenericResponse(Exception exc)
        {
            _success = false;
            _message = null;
            _exception = exc;
        }

        public virtual bool Success
        {
            get { return _success; }
            set
            {
                _success = value;
                if (_success)
                {
                    _exception = null;
                }
            }
        }

        public virtual string Message
        {
            get { return _success ? _message : ((_exception == null) ? "Unknown exception" : _exception.Message); }
        }

        public virtual Exception Exception
        {
            get
            {
                var _exc = _exception ?? new Exception(_message);
                return _exc;
            }
        }
    }
}

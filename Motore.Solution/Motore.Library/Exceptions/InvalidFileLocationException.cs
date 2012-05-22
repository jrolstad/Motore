using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Library.Exceptions
{
    public class InvalidFileLocationException : Exception
    {
        public InvalidFileLocationException(string msg) : base(msg)
        {
            
        }
    }
}

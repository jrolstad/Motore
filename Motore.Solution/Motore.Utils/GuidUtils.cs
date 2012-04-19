using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils
{
    public static class GuidUtils
    {
        public static string NewToken()
        {
            return Guid.NewGuid().Condense();
        }

        public static string Condense(this Guid guid)
        {
            return guid.ToString().Replace("-", "");
        }
        
    }
}

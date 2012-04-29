using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Text
{
    public class Regex
    {
        public static bool IsMatch(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Text
{
    public static class StringExtensions
    {
        public static bool BeginsWithNumber(this string input)
        {
            const string pattern = @"^[\d]+.*";
            return Regex.IsMatch(input, pattern);
        }
    }
}

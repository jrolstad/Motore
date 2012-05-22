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

        public static bool IsUri(string input)
        {
            var isUri = false;
            try
            {
                new Uri(input);
                isUri = true;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return isUri;
        }

        public static string GetValueBeforeFirstSlash(string input)
        {
            var result = "";
            if (!String.IsNullOrEmpty(input))
            {
                const string pattern = "^(?<begin>[^/]*).*$";
                var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
                if (match.Success)
                {
                    result = match.Groups["begin"].Value;
                }
            }
            return result;

        }

        public static string GetValueAfterFirstSlash(string input)
        {
            var result = "";
            if (!String.IsNullOrEmpty(input))
            {
                const string pattern = "^(?<begin>[^/]*)/(?<after>.*)$";
                var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
                if (match.Success)
                {
                    result = match.Groups["after"].Value;
                }
                else
                {
                    result = input;
                }
            }
            return result;

        }
    }
}

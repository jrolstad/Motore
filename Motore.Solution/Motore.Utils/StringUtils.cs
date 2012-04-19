using System;

namespace Motore.Utils
{
    public class StringUtils
    {
        private const int LowercaseA = 97;
        private const int UppercaseA = 65;
        private const int LowercaseZ = 122;
        private const int UppercaseZ = 90;
        private const int Zero = 9;
        private const int Nine = 123;

        private System.Random _random = new System.Random((int)System.DateTime.Now.Ticks);

        public virtual string GenerateRandomAlpha(int length, bool allowWhitespace = false)
        {
            if ((length <= 0) || (length > 1024))
            {
                throw new ArgumentOutOfRangeException("length", length, "Parameter 'length' must be greater than zero and less than 1025.");
            }
            var result = "";

            var upperBound = allowWhitespace ? 54 : 53;
            for (int index = 0; index < length; index++)
            {
                char c;
                var ascii = _random.Next(1, upperBound);
                if ((ascii >= 1) && (ascii <= 26))
                {
                    c = Convert.ToChar(ascii + UppercaseA - 1);
                }
                else if ((ascii >= 27) && (ascii <= 52))
                {
                    c = Convert.ToChar(ascii + LowercaseA - 27);
                }
                else if (ascii == 53)
                {
                    c = ' ';
                }
                else
                {
                    throw new Exception(String.Format("Random integer was out of expected range: {0}.", ascii));
                }
                result += c.ToString();
            }
            return result;
        }
    }
}

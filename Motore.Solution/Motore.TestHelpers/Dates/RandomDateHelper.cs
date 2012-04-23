using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.TestHelpers.Dates
{
    public static class RandomDateHelper
    {
        private readonly static Random _rand = new System.Random();

        public static DateTime GetRandomRecentDate()
        {
            var toSubtract = _rand.Next(10, 40);
            return DateTime.Now.AddDays(-toSubtract);
        }
    }
}

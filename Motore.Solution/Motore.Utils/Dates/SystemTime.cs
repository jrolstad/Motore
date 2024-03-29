﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motore.Utils.Dates
{
    public static class SystemTime
    {

#if DEBUG
        public static Func<DateTime> Now = () => DateTime.UtcNow;
#else
        public static DateTime Now()
        {
            return DateTime.UtcNow;
        }
#endif

    }
}

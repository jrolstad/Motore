using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Motore.Library.Logging
{
    public class Log
    {
        /// <summary>
        /// Logger used for logging the error
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Motore.Library.Logging.Log));

        // YAGNI
        public static void WriteDebug(string message)
        {
            var logger = GetLogger();
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }
        public static void WriteInfo(string message)
        {
            var logger = GetLogger();
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        public static void LogException(Exception exc)
        {
            LogException(exc.ToString());
        }

        public static void LogException(string message)
        {
            var logger = GetLogger();
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        #region Private Methods

        private static ILog GetLogger()
        {
            return _logger;
        }

        #endregion

    }
}

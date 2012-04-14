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
        private readonly ILog _logger = LogManager.GetLogger(typeof(Motore.Library.Logging.Log));

        // YAGNI
        public virtual void WriteDebug(string message)
        {
            var logger = GetLogger();
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        public virtual void LogException(Exception exc)
        {
            var logger = GetLogger();
            if (logger.IsErrorEnabled)
            {
                logger.Error(exc.ToString());
            }
        }

        private ILog GetLogger()
        {
            return _logger;
        }
    }
}

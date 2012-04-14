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
            logger.Debug(message);
        }

        private ILog GetLogger()
        {
            return _logger;
        }
    }
}

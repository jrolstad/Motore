using System.Web.Mvc;
using log4net;

namespace Motore.Performance.Web.Attributes
{
    /// <summary>
    /// When an exception occurs, logs it
    /// </summary>
    public class HandleErrorAndLogAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Logger used for logging the error
        /// </summary>
        private readonly ILog _logger = LogManager.GetLogger(typeof(HandleErrorAndLogAttribute));

        /// <summary>
        /// When an exception occurs
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            _logger.Error(filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}
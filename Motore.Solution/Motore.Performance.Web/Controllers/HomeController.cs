using System.Web.Mvc;

namespace Motore.Performance.Web.Controllers
{
    /// <summary>
    /// Controller for the home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Shows the main site / landing page
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Motore.Library.Aws.SimpleDb;
using Motore.Performance.Web.Attributes;
using log4net.Config;

namespace Motore.Performance.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAndLogAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ReportWizardStep", // Route name
                "ReportWizard/{token}/Step/{stepNumber}", // URL with parameters
                new
                    {
                        controller = "ReportWizard",
                        action = "WizardStep",
                        token = UrlParameter.Optional,
                        stepNumber = UrlParameter.Optional,
                    }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            // this is for log4net
            XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            this.InitializeSimpleDbDomains();
        }

        protected internal virtual void InitializeSimpleDbDomains()
        {
            var initializer = new DomainInitializer();
            initializer.Initialize();
        }
    }
}
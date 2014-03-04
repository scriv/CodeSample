using DaveScriven.CodeSample.Site.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DaveScriven.CodeSample.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.InitialiseMappings();
            EventStoreDatabaseInitializer.Initialize();
        }

        protected void Application_End()
        {
            // Shutdown the CQRS runtime
            if (Bootstrapper.Container != null)
            {
                Bootstrapper.Container.GetInstance<CqrsRuntime>().Shutdown();
            }
        }
    }
}

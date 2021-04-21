using PegasusPlus.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PegasusPlus
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // not required - throws exception
            //ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);

            // this solved the issue of the report route error (/api/reports/)
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Golf_Results_MVC.DAL;
using System.Data.Entity.Infrastructure.Interception;

namespace Golf_Results_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register); // this added for WebApi
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            /*These lines of code below are what causes your interceptor code 
             * to be run when Entity Framework sends queries to the database.*/
            DbInterception.Add(new GolfInterceptorTransientErrors());
            DbInterception.Add(new GolfInterceptorLogging());
        }
    }
}

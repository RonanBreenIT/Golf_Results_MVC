using System.Web.Mvc;
using System.Web.Routing;

namespace Golf_Results_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{season}", // added season if split results by season
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, season = UrlParameter.Optional }
            );
        }
    }
}

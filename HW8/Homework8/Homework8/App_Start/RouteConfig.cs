using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homework8
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "StudentResult", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GraphInfo",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "GraphMath", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TeamInfo",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TeamInfo", id = UrlParameter.Optional }
);

        }
    }
}

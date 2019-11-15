using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homework7
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "GitUserInfo",
               url: "api/{action}/{id}",
               defaults: new { controller = "Home", action = "user", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "GitRepoInfo",
               url: "api/{action}/{id}",
               defaults: new { controller = "Home", action = "repositories", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

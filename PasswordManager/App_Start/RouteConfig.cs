using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PasswordManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "MovieList",
                url: "Movies",
                defaults: new { controller = "Movie", action = "Index" }
            );

            routes.MapRoute(
                name: "Movies",
                url: "Movie/{action}/{id}",
                defaults: new { controller = "Movie", action = "Index", id = 0 }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}

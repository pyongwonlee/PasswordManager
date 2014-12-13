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

            routes.MapRoute(
                name: "Search",
                url: "Search/{searchTerm}",
                defaults: new { controller = "Home", action = "Search", searchTerm = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Categories",
                url: "Category/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = 0 }
            );

            routes.MapRoute(
                name: "CompanyList",
                url: "Companies/{page}",
                defaults: new { controller = "Company", action = "Index", page = 1 }
            );

            routes.MapRoute(
                name: "Companies",
                url: "Company/{action}/{id}",
                defaults: new { controller = "Company", action = "Index", id = 0 }
            );

            routes.MapRoute(
                name: "PasswordList",
                url: "Passwords",
                defaults: new { controller = "Password", action = "Index" }
            );

            routes.MapRoute(
                name: "Passwords",
                url: "Password/{action}/{id}",
                defaults: new { controller = "Password", action = "Index", id = 0 }
            );

            routes.MapRoute(
                name: "Directors",
                url: "Director/{action}/{id}",
                defaults: new { controller = "Director", action = "Index", id = 0 }
            );

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

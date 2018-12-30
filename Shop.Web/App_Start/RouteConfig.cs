using System.Web.Mvc;
using System.Web.Routing;

namespace Shop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "About",
               url: "gioi-thieu",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Product Category",
               url: "{alias}.pc-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Product",
               url: "{alias}.p-{id}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
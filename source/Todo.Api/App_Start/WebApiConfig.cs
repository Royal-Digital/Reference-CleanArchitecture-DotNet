using System.Net.Http.Headers;
using System.Web.Http;

namespace Todo.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            MapRoutes(config);

            ConfigureMediaType(config);
        }

        private static void ConfigureMediaType(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );
        }
    }
}

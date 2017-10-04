using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Todo.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            OverrideControllerTypeResolver(config);
            OverrideControllerConstant();

            MapRoutes(config);

            ConfigureMediaType(config);
        }

        private static void ConfigureMediaType(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        private static void OverrideControllerConstant()
        {
            var suffix =
                typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null) suffix.SetValue(null, string.Empty);
        }

        private static void OverrideControllerTypeResolver(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new CustomHttpControllerTypeResolver());
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

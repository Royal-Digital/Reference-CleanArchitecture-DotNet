using System.Web;
using System.Web.Http;
using Swashbuckle.Application;
using System.Reflection;
using Todo.Api;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace Todo.Api
{
    public class AssemblyInfo
    {
        public string Version { get; set; }
        public string ApplicationName { get; set; }
    }

    public static class SwaggerConfig
    {
        public static void Register()
        {
            var assemblyInfo = GetAssemblyVersion();

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion($"v1", $"{assemblyInfo.ApplicationName}");
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DisableValidator();
                        c.DocExpansion(DocExpansion.List); 
                        c.EnableDiscoveryUrlSelector();
                    });
        }

        private static AssemblyInfo GetAssemblyVersion()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            var assemblyInfo = new AssemblyInfo
            {
                Version = GetVersion(thisAssembly),
                ApplicationName = GetTitle(thisAssembly)
            };

            return assemblyInfo;
        }

        private static string GetTitle(Assembly thisAssembly)
        {
            var attribute = thisAssembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var title = attribute.Title;
            return title;
        }

        private static string GetVersion(Assembly thisAssembly)
        {
            var version = thisAssembly.GetName().Version.ToString();
            return version;
        }
    }
}

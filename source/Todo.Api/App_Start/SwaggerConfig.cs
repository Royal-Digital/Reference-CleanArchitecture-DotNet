using System.Web;
using System.Web.Http;
using SparData;
using System.Web.Http.Description;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SparData
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Todo Api - Clean Architecture Example");
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DisableValidator();
                        c.DocExpansion(DocExpansion.List); 
                        c.EnableDiscoveryUrlSelector();
                    });

        }

        private static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            return apiDesc.Route.RouteTemplate.Contains(targetApiVersion);
        }
    }
}

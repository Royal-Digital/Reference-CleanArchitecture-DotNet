using System.Web.Http;
using Todo.Api;

namespace Todo.Api2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            IocConfig.Configure(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            JsonFormatterConfig.Configure();
        }
    }
}

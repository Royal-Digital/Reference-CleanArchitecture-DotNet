using System.Web.Http;

namespace Todo.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            IocConfig.Configure(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

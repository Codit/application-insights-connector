using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin;
using Owin;
using TomKerkhove.Connectors.ApplicationInsights;
using TomKerkhove.Connectors.ApplicationInsights.Loggers;

[assembly: OwinStartup(typeof(OwinStartup))]
namespace TomKerkhove.Connectors.ApplicationInsights
{
    public class OwinStartup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;

            ConfigureRoutes(httpConfiguration);
            ConfigureLoggers(httpConfiguration);
            ConfigureSwagger();

            httpConfiguration.EnsureInitialized();
        }

        private void ConfigureRoutes(HttpConfiguration httpConfiguration)
        {
            WebApiConfig.Register(httpConfiguration);
        }

        private void ConfigureLoggers(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Add(typeof(IExceptionLogger), new OwinExceptionLogger());
        }

        private void ConfigureSwagger()
        {
            SwaggerConfig.Register();
        }
    }
}
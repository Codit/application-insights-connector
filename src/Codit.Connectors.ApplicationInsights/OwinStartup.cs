using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin;
using Owin;
using Codit.Connectors.ApplicationInsights;
using Codit.Connectors.ApplicationInsights.Configuration;
using Codit.Connectors.ApplicationInsights.ExceptionHandling;

[assembly: OwinStartup(typeof(OwinStartup))]
namespace Codit.Connectors.ApplicationInsights
{
    public class OwinStartup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;

            ConfigureRoutes(httpConfiguration);
            ConfigureExceptionHandling(httpConfiguration);
            ConfigureSwagger();

            httpConfiguration.EnsureInitialized();
        }

        private void ConfigureRoutes(HttpConfiguration httpConfiguration)
        {
            WebApiConfig.Register(httpConfiguration);
        }

        private void ConfigureExceptionHandling(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new OwinExceptionHandler());
            httpConfiguration.Services.Add(typeof(IExceptionLogger), new ApplicationInsightsExceptionLogger());
        }

        private void ConfigureSwagger()
        {
            SwaggerConfig.Register();
        }
    }
}
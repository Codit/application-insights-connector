using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using TomKerkhove.Connectors.ApplicationInsights.Configuration;

namespace TomKerkhove.Connectors.ApplicationInsights.Loggers
{
    public class OwinExceptionLogger : IExceptionLogger
    {
        private readonly ApplicationInsightsTelemetry _applicationInsightsTelemetry;
        public OwinExceptionLogger()
        {
            string instrumentationKey = ConfigurationProvider.GetSetting(Constants.Configuration.RuntimeInstrumentationKeySettingName);
            _applicationInsightsTelemetry = new ApplicationInsightsTelemetry(instrumentationKey);
        }

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            if (context.Exception != null)
            {
                LogToApplicationInsights(context);
            }

            return Task.CompletedTask;
        }

        private void LogToApplicationInsights(ExceptionLoggerContext context)
        {
            var customProperties = new Dictionary<string, string>();

            if (context.Request != null)
            {
                var correlationId = context.Request.GetCorrelationId();
                customProperties.Add("CorrelationId", correlationId.ToString());
            }

            var exceptionMessage = context.Exception.Message;
            customProperties.Add("ExceptionMessage", exceptionMessage);

            _applicationInsightsTelemetry.TrackException(context.Exception, customProperties);
        }
    }
}
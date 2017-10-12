using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Codit.Connectors.ApplicationInsights.Configuration;

namespace Codit.Connectors.ApplicationInsights.ExceptionHandling
{
    public class ApplicationInsightsExceptionLogger : IExceptionLogger
    {
        private readonly ApplicationInsightsTelemetry applicationInsightsTelemetry;

        public ApplicationInsightsExceptionLogger()
        {
            string instrumentationKey = ConfigurationProvider.GetSetting(Constants.Configuration.Telemetry.RuntimeInstrumentationKeySettingName);
            applicationInsightsTelemetry = new ApplicationInsightsTelemetry(instrumentationKey);
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

            applicationInsightsTelemetry.TrackException(context.Exception, customProperties);
        }
    }
}
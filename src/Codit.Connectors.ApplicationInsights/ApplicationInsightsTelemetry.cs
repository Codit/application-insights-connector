using System;
using System.Collections.Generic;
using Codit.Connectors.ApplicationInsights.Configuration;
using Codit.Connectors.ApplicationInsights.Exceptions;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Codit.Connectors.ApplicationInsights
{
    using Guard;

    public class ApplicationInsightsTelemetry
    {
        private const string DefaultInstrumentationKey = "_APPLICATION-INSIGHTS-INSTRUMENTATION-KEY_";
        private readonly TelemetryClient telemetryClient;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="instrumentationKey">Instrumentation key to use</param>
        public ApplicationInsightsTelemetry(string instrumentationKey)
        {
            InstrumentationKey = DetermineTelemetryKey(instrumentationKey);

            telemetryClient = new TelemetryClient
            {
                InstrumentationKey = InstrumentationKey
            };
        }

        /// <summary>
        ///     Instrumentation key used to write to Azure Application Insights
        /// </summary>
        public string InstrumentationKey { get; }

        /// <summary>
        ///     Write an event to Application Insights
        /// </summary>
        /// <param name="name">Name of the event occuring</param>
        /// <param name="customProperties">Custom properties that provide context for the specific event</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackEvent(string name, Dictionary<string, string> customProperties)
        {
            Guard.NotNullOrWhitespace(name, nameof(name));
            Guard.NotNull(customProperties, nameof(customProperties));

            telemetryClient.TrackEvent(name, customProperties);
        }

        /// <summary>
        ///     Writes an exception to Application Insights
        /// </summary>
        /// <param name="exception">Exception that occured</param>
        /// <param name="customProperties">Custom properties that provide context for the specific exception</param>
        public void TrackException(Exception exception, Dictionary<string, string> customProperties)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(customProperties, nameof(customProperties));

            telemetryClient.TrackException(exception, customProperties);
        }

        /// <summary>
        ///     Write an metric to Application Insights
        /// </summary>
        /// <param name="name">Name of the metric</param>
        /// <param name="value">Value of the metric</param>
        /// <param name="customProperties">Custom properties that provide context for the specific metric</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackMetric(string name, double value, Dictionary<string, string> customProperties)
        {
            Guard.NotNullOrWhitespace(name, nameof(name));
            Guard.NotNull(customProperties, nameof(customProperties));

            telemetryClient.TrackMetric(name, value, customProperties);
        }

        /// <summary>
        ///     Write an metric to Application Insights
        /// </summary>
        /// <param name="name">Name of the metric</param>
        /// <param name="sum">Total sum of all samples taken in this metric</param>
        /// <param name="count">Total count of samples in this metric</param>
        /// <param name="max">Maximum value of this metric</param>
        /// <param name="min">Minimum value of this metric</param>
        /// <param name="standardDeviation">Standard deviation of this metric</param>
        /// <param name="customProperties">Custom properties that provide context for the specific metric</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackSampledMetric(string name, double sum, int? count, double? max, double? min,
            double? standardDeviation, Dictionary<string, string> customProperties)
        {
            Guard.NotNullOrWhitespace(name, nameof(name));
            Guard.NotNull(customProperties, nameof(customProperties));

            var metricTelemetry = new MetricTelemetry
            {
                Name = name,
                Sum = sum,
                Count = count,
                Max = max,
                Min = min,
                StandardDeviation = standardDeviation
            };

            metricTelemetry.Properties.AddRange(customProperties);

            telemetryClient.TrackMetric(metricTelemetry);
        }

        /// <summary>
        ///     Write a trace to Application Insights
        /// </summary>
        /// <param name="message">Message to trace</param>
        /// <param name="severityLevel">Severity level of the trace</param>
        /// <param name="customProperties">Custom properties that provide context for the specific trace</param>
        /// <exception cref="ArgumentNullException">Exception thrown when parameters are not valid</exception>
        public void TrackTrace(string message, SeverityLevel severityLevel, Dictionary<string, string> customProperties)
        {
            Guard.NotNullOrWhitespace(message, nameof(message));
            Guard.NotNull(customProperties, nameof(customProperties));

            telemetryClient.TrackTrace(message, severityLevel, customProperties);
        }

        private string DetermineTelemetryKey(string instrumentationKey)
        {
            var determinedInstrumentationKey = string.IsNullOrWhiteSpace(instrumentationKey)
                ? ConfigurationProvider.GetSetting(Constants.Configuration.Telemetry.DefaultInstrumentationKeySettingName)
                : instrumentationKey;

            if (string.Equals(determinedInstrumentationKey, DefaultInstrumentationKey, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InstrumentationKeyNotSpecifiedException();
            }

            return determinedInstrumentationKey;
        }
    }
}
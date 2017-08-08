using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Codit.Connectors.ApplicationInsights.Configuration;

namespace Codit.Connectors.ApplicationInsights
{
    public class ApplicationInsightsTelemetry
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplicationInsightsTelemetry() : this(string.Empty)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="instrumentationKey">Instrumentation key to use</param>
        public ApplicationInsightsTelemetry(string instrumentationKey)
        {
            InstrumentationKey = string.IsNullOrWhiteSpace(instrumentationKey)
                ? ConfigurationProvider.GetSetting(Constants.Configuration.DefaultInstrumentationKeySettingName)
                : instrumentationKey;

            _telemetryClient = new TelemetryClient
            {
                InstrumentationKey = InstrumentationKey
            };
        }

        /// <summary>
        ///     Instrumentation key used to write to Azure Application Insights
        /// </summary>
        public string InstrumentationKey { get; }

        /// <summary>
        ///     Write an metric to Application Insights
        /// </summary>
        /// <param name="name">Name of the metric</param>
        /// <param name="value">Value of the metric</param>
        /// <param name="customProperties">Custom properties that provide context for the specific metric</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackMetric(string name, double value, Dictionary<string, string> customProperties)
        {
            Guard.AgainstNullOrWhitespace(name, nameof(name));
            Guard.AgainstNull(customProperties, nameof(customProperties));

            _telemetryClient.TrackMetric(name, value, customProperties);
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
            Guard.AgainstNullOrWhitespace(name, nameof(name));
            Guard.AgainstNull(customProperties, nameof(customProperties));

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

            _telemetryClient.TrackMetric(metricTelemetry);
        }

        /// <summary>
        ///     Write an event to Application Insights
        /// </summary>
        /// <param name="name">Name of the event occuring</param>
        /// <param name="customProperties">Custom properties that provide context for the specific event</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackEvent(string name, Dictionary<string, string> customProperties)
        {
            Guard.AgainstNullOrWhitespace(name, nameof(name));
            Guard.AgainstNull(customProperties, nameof(customProperties));

            _telemetryClient.TrackEvent(name, customProperties);
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
            Guard.AgainstNullOrWhitespace(message, nameof(message));
            Guard.AgainstNull(customProperties, nameof(customProperties));

            _telemetryClient.TrackTrace(message, severityLevel, customProperties);
        }

        /// <summary>
        ///     Writes an exception to Application Insights
        /// </summary>
        /// <param name="exception">Exception that occured</param>
        /// <param name="customProperties">Custom properties that provide context for the specific exception</param>
        public void TrackException(Exception exception, Dictionary<string, string> customProperties)
        {
            Guard.AgainstNull(exception, nameof(exception));
            Guard.AgainstNull(customProperties, nameof(customProperties));

            _telemetryClient.TrackException(exception, customProperties);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace TomKerkhove.Connectors.ApplicationInsights
{
    public class ApplicationInsightsTelemetry
    {
        private const string InstrumentationKeySettingName = "ApplicationInsights.InstrumentationKey";
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        ///     Instrumentation key used to write to Azure Application Insights
        /// </summary>
        public string InstrumentationKey { get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplicationInsightsTelemetry() : this(instrumentationKey: string.Empty)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="instrumentationKey">Instrumentation key to use</param>
        public ApplicationInsightsTelemetry(string instrumentationKey)
        {
            InstrumentationKey = string.IsNullOrWhiteSpace(instrumentationKey)
                ? GetInstrumentationKey()
                : instrumentationKey;

            _telemetryClient = new TelemetryClient
            {
                InstrumentationKey = InstrumentationKey
            };
        }

        /// <summary>
        ///     Write a custom event to Application Insights
        /// </summary>
        /// <param name="eventName">Name of the custom event occuring</param>
        /// <exception cref="ArgumentNullException">Exception thrown when event name was not valid</exception>
        public void TrackCustomEvent(string eventName)
        {
            Guard.AgainstNullOrWhitespace(eventName, nameof(eventName));

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Write a trace to Application Insights
        /// </summary>
        /// <param name="message">Message to trace</param>
        /// <param name="severityLevel">Severity level of the trace</param>
        /// <exception cref="ArgumentNullException">Exception thrown when message was not valid</exception>
        public void TrackTrace(string message, SeverityLevel severityLevel)
        {
            Guard.AgainstNullOrWhitespace(message, nameof(message));

            TrackTrace(message, severityLevel, new Dictionary<string, string>());
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

        private string GetInstrumentationKey()
        {
            var instrumentationKey = ConfigurationManager.AppSettings[InstrumentationKeySettingName];

            if (string.IsNullOrWhiteSpace(instrumentationKey))
            {
                throw new InvalidOperationException($"Instrumentation key was not configured with setting {InstrumentationKeySettingName}");
            }

            return instrumentationKey;
        }
    }
}
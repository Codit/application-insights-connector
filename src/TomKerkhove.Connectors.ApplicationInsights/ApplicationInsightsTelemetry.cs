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

        /// <summary>
        /// Write a trace to Application Insights
        /// </summary>
        /// <param name="message">Message to trace</param>
        /// <param name="severityLevel">Severity level of the trace</param>
        /// <exception cref="ArgumentNullException">Exception thrown when message was not valid</exception>
        public static void Trace(string message, SeverityLevel severityLevel)
        {
            Guard.AgainstNullOrWhitespace(message, nameof(message));

            Trace(message, severityLevel, new Dictionary<string, string>());
        }

        /// <summary>
        /// Write a trace to Application Insights
        /// </summary>
        /// <param name="message">Message to trace</param>
        /// <param name="severityLevel">Severity level of the trace</param>
        /// <param name="customProperties">Custom properties that provide context for the specific trace</param>
        /// <exception cref="ArgumentNullException">Exception thrown when parameters are not valid</exception>
        public static void Trace(string message, SeverityLevel severityLevel, Dictionary<string, string> customProperties)
        {
            Guard.AgainstNullOrWhitespace(message, nameof(message));
            Guard.AgainstNull(customProperties, nameof(customProperties));

            var telemetryId = GetInstrumentationKey();
            var telemetryClient = new TelemetryClient
            {
                InstrumentationKey = telemetryId
            };

            telemetryClient.TrackTrace(message, severityLevel, customProperties);
        }

        private static string GetInstrumentationKey()
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
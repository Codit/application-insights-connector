using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.ApplicationInsights;

namespace TomKerkhove.Connectors.ApplicationInsights
{
    public class ApplicationInsightsTelemetry
    {
        private const string InstrumentationKeySettingName = "ApplicationInsights.InstrumentationKey";

        public static void Trace(string message)
        {
            // TODO: Add validation 


            Trace(message, new Dictionary<string, string>());
        }

        public static void Trace(string message, Dictionary<string, string> customProperties)
        {
            // TODO: Add validation 

            var telemetryId = GetInstrumentationKey();
            var telemetryClient = new TelemetryClient
            {
                InstrumentationKey = telemetryId
            };

            telemetryClient.TrackTrace(message, customProperties);
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
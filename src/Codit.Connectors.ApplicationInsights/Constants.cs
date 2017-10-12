namespace Codit.Connectors.ApplicationInsights
{
    public static class Constants
    {
        public static class Configuration
        {
            public static class Telemetry
            {
                public const string RuntimeInstrumentationKeySettingName = "ApplicationInsights.Runtime.InstrumentationKey";
                public const string DefaultInstrumentationKeySettingName = "ApplicationInsights.InstrumentationKey";
                public const string DefaultInstrumentationKeySettingValue = "_APPLICATION-INSIGHTS-INSTRUMENTATION-KEY_";
            }
            
            public static class Authentication
            {
                public static class SharedAccessKey
                {
                    public const string EnabledSettingName = "Authentication.EnableSharedAccessKey";
                    public const string HeaderSettingName = "Authentication.SharedAccessKeyHeaderName";
                    public const string PoolSettingName = "Authentication.AccessKeyPool";
                }
            }
        }
        public static class Errors
        {
            public const string MissingInstrumentationKey = "InstrumentationKey is missing.";
        }
    }
}
namespace Codit.Connectors.ApplicationInsights
{
    public static class Constants
    {
        public static class Configuration
        {
            public const string RuntimeInstrumentationKeySettingName = "ApplicationInsights.Runtime.InstrumentationKey";
            public const string DefaultInstrumentationKeySettingName = "ApplicationInsights.InstrumentationKey";
            public const string AuthenticationEnabledKeySettingName = "Authentication.EnableSharedAccessKey";
            public const string AuthenticationHeaderKeySettingName = "Authentication.SharedAccessKeyHeaderName";
            public const string AuthenticationAccessKeyPoolKeySettingName = "Authentication.AccessKeyPool";
            public const string DefaultInstrumentationKeySettingValue = "_APPLICATION-INSIGHTS-INSTRUMENTATION-KEY_";
        }
        public static class Errors
        {
            public const string MissingInstrumentationKey = "InstrumentationKey is missing.";
        }
    }
}
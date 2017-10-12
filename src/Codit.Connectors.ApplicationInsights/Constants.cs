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
            public const string MissingInstrumentationKey = "No instrumentation key was specified or configured";
        }
    }
}
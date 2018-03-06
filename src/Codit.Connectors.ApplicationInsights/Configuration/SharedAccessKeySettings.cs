namespace Codit.Connectors.ApplicationInsights.Configuration
{
    public class SharedAccessKeySettings
    {
        public static string GetHeaderName()
        {
            return ConfigurationProvider.GetSetting(Constants.Configuration.Authentication.SharedAccessKey.HeaderSettingName);
        }
        public static bool IsEnabled()
        {
            var rawSetting = ConfigurationProvider.GetSetting(Constants.Configuration.Authentication.SharedAccessKey.EnabledSettingName);
            return bool.TryParse(rawSetting, out var isSharedAccessKeyEnabled) && isSharedAccessKeyEnabled;
        }

        public static string AccessKeyPool()
        {
            return ConfigurationProvider.GetSetting(Constants.Configuration.Authentication.SharedAccessKey.PoolSettingName);
        }
    }
}
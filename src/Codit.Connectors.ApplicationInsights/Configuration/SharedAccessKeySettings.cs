using System.Configuration;


namespace Codit.Connectors.ApplicationInsights.Configuration
{
    public class SharedAccessKeySettings
    {
        public static string SharedAccessKeyHeaderName()
        {
            return ConfigurationProvider.GetSetting(Constants.Configuration.AuthenticationHeaderKeySettingName);
        }
        public static bool IsSharedAccessKeyEnabled()
        {
            bool isSharedAccessKeyEnabled;

            var rawSetting = ConfigurationProvider.GetSetting(Constants.Configuration.AuthenticationEnabledKeySettingName);
            return bool.TryParse(rawSetting, out isSharedAccessKeyEnabled) ? isSharedAccessKeyEnabled : false;
        }

        public static string AccessKeyPool()
        {
            return ConfigurationProvider.GetSetting(Constants.Configuration.AuthenticationAccessKeyPoolKeySettingName);
        }
    }
}
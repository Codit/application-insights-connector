using System.Configuration;

namespace Codit.Connectors.ApplicationInsights.Configuration
{
    public class ConfigurationProvider
    {
        /// <summary>
        /// Gets the value for a specific setting
        /// </summary>
        /// <param name="settingName">Name of the setting</param>
        /// <returns>Configured value for the setting</returns>
        public static string GetSetting(string settingName)
        {
            Guard.AgainstNullOrWhitespace(settingName, nameof(settingName));

            var settingValue = ConfigurationManager.AppSettings[settingName];

            return settingValue;
        }
    }

    public class Settings
    {
        public static string SharedAccessKeyHeaderName()
        {
            return ConfigurationProvider.GetSetting("Authentication.SharedAccessKeyHeaderName");
        }
        public static bool IsSharedAccessKeyEnabled()
        {
            bool isSharedAccessKeyEnabled;

            var rawSetting = ConfigurationProvider.GetSetting("Authentication.EnableSharedAccessKey");
            return bool.TryParse(rawSetting, out isSharedAccessKeyEnabled) ? isSharedAccessKeyEnabled : true;
        }

        public static string KeyPool()
        {
            return ConfigurationProvider.GetSetting("Authentication.KeyPool");
        }
    }
}
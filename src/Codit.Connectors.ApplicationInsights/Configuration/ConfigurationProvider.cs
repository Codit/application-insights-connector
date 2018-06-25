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
            Guard.Guard.NotNullOrWhitespace(settingName, nameof(settingName));

            var settingValue = ConfigurationManager.AppSettings[settingName];

            return settingValue;
        }
    }
 }
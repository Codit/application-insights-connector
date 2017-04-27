using System;
using System.Runtime.Serialization;

namespace TomKerkhove.Connectors.ApplicationInsights.Configuration
{
    [Serializable]
    [DataContract]
    public class SettingNotFoundException : Exception
    {
        [DataMember]
        public string SettingName { get; }

        public SettingNotFoundException(string settingName) : base($"Setting with name '{settingName}' was not configured")
        {
            SettingName = settingName;
        }

        public SettingNotFoundException(string settingName, Exception inner) : base($"Setting with name '{settingName}' was not configured", inner)
        {
            SettingName = settingName;
        }

        protected SettingNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
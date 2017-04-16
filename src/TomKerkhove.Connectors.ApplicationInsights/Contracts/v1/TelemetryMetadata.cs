using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TomKerkhove.Connectors.ApplicationInsights.Contracts.v1
{
    [DataContract]
    public class TelemetryMetadata
    {
        /// <summary>
        ///     Instrumentation key of the Application Insights instance to send to (Optional)
        /// </summary>
        [DataMember]
        public string InstrumentationKey { get; set; }

        /// <summary>
        ///     Context about a specific trace (Optional)
        /// </summary>
        [DataMember]
        public Dictionary<string, string> CustomProperties { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return $"{nameof(InstrumentationKey)}: {InstrumentationKey}, {nameof(CustomProperties)}: {string.Join(", ", CustomProperties?.Select(entry => $"{entry.Key}={entry.Value}").ToArray())}";
        }
    }
}
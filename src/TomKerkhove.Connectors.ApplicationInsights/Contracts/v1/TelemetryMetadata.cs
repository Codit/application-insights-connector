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
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.ApplicationInsights.DataContracts;

namespace TomKerkhove.Connectors.ApplicationInsights.Contracts.v1
{
    /// <summary>
    ///     Metadata about a trace to be made
    /// </summary>
    [DataContract]
    public class TraceMetadata : TelemetryMetadata
    {
        /// <summary>
        ///     Message to trace
        /// </summary>
        [Required]
        [DataMember]
        [DefaultValue("Message to trace")]
        public string Message { get; set; }

        /// <summary>
        ///     Severity level of the trace. Informational by default
        /// </summary>
        [DataMember]
        [DefaultValue(SeverityLevel.Information)]
        public SeverityLevel SeverityLevel { get; set; } = SeverityLevel.Information;

        public override string ToString()
        {
            return $"{nameof(TraceMetadata)} - {nameof(Message)}: \"{Message}\", {nameof(SeverityLevel)}: {SeverityLevel}. General metadata: {base.ToString()}";
        }
    }
}
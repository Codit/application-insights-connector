using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Codit.Connectors.ApplicationInsights.Contracts.v1
{
    [DataContract]
    public class MetricMetadata : TelemetryMetadata
    {
        /// <summary>
        ///     Name of the metric
        /// </summary>
        [Required]
        [DataMember]
        [DefaultValue("Name of the metric")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(EventMetadata)} - {nameof(Name)}: {Name}. General metadata: {base.ToString()}";
        }
    }
}
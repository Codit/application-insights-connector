using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Codit.Connectors.ApplicationInsights.Contracts.v1
{
    [DataContract]
    public class BasicMetricMetadata:MetricMetadata
    {
        /// <summary>
        ///     Value of the metric
        /// </summary>
        [Required]
        [DataMember]
        [DefaultValue(0)]
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(EventMetadata)} - {nameof(Name)}: {Name}, {nameof(Value)}: {Value}. General metadata: {base.ToString()}";
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TomKerkhove.Connectors.ApplicationInsights.Contracts.v1
{
    [DataContract]
    public class SampledMetricMetadata : MetricMetadata
    {
        /// <summary>
        ///     Total sum of all samples taken in this metric
        /// </summary>
        [Required]
        [DataMember]
        public double? Sum { get; set; }

        /// <summary>
        ///     Total count of samples in this metric
        /// </summary>
        [DataMember]
        public int? Count { get; set; }

        /// <summary>
        ///     Maximum value of this metric
        /// </summary>
        [DataMember]
        public double? Max { get; set; }

        /// <summary>
        ///     Minimum value of this metric
        /// </summary>
        [DataMember]
        public double? Min { get; set; }

        /// <summary>
        ///     Standard deviation of this metric
        /// </summary>
        [DataMember]
        public double? StandardDeviation { get; set; }

        public override string ToString()
        {
            return $"{nameof(EventMetadata)} - {nameof(Name)}: {Name}, {nameof(Sum)}: {Sum}, {nameof(Count)}: {Count}, {nameof(Max)}: {Max}, {nameof(Min)}: {Min}, {nameof(StandardDeviation)}: {StandardDeviation}. General metadata: {base.ToString()}";
        }
    }
}
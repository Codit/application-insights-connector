using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace TomKerkhove.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    public class MetricsController : ApiController
    {
        /// <summary>
        /// Tracks a custom metric to Azure Application Insights
        /// </summary>
        /// <param name="metricMetadata">Metadata concerning the metric to track</param>
        [HttpPost]
        [Route("metics")]
        [SwaggerResponse(HttpStatusCode.NoContent, description: "Metric was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, description: "Specified metric metadata was invalid")]
        public IHttpActionResult Metric([FromBody]Contracts.v1.BasicMetricMetadata metricMetadata)
        {
            if (metricMetadata == null)
            {
                return BadRequest("No metadata about the metric was specified");
            }
            if (string.IsNullOrWhiteSpace(metricMetadata.Name))
            {
                return BadRequest("No metric name was specified");
            }

            var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(metricMetadata.InstrumentationKey);
            applicationInsightsTelemetry.TrackMetric(metricMetadata.Name, metricMetadata.Value, metricMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Tracks a custom metric with sampling to Azure Application Insights
        /// </summary>
        /// <param name="metricMetadata">Metadata concerning the metric to track</param>
        [HttpPost]
        [Route("metics/sampling")]
        [SwaggerResponse(HttpStatusCode.NoContent, description: "Metric was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, description: "Specified metric metadata was invalid")]
        public IHttpActionResult Metric([FromBody]Contracts.v1.SampledMetricMetadata metricMetadata)
        {
            if (metricMetadata == null)
            {
                return BadRequest("No metadata about the metric was specified");
            }
            if (string.IsNullOrWhiteSpace(metricMetadata.Name))
            {
                return BadRequest("No metric name was specified");
            }
            if (metricMetadata.Sum == null)
            {
                return BadRequest("No sum was specified");
            }

            var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(metricMetadata.InstrumentationKey);
            applicationInsightsTelemetry.TrackSampledMetric(metricMetadata.Name, metricMetadata.Sum.Value, metricMetadata.Count, metricMetadata.Max, metricMetadata.Min, metricMetadata.StandardDeviation, metricMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

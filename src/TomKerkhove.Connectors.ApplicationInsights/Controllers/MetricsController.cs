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
        public IHttpActionResult Metric([FromBody]Contracts.v1.MetricMetadata metricMetadata)
        {
            if (metricMetadata == null)
            {
                return BadRequest("No metadata about the event was specified");
            }
            if (string.IsNullOrWhiteSpace(metricMetadata.Name))
            {
                return BadRequest("No event name was specified");
            }

            var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(metricMetadata.InstrumentationKey);
            applicationInsightsTelemetry.TrackMetric(metricMetadata.Name, metricMetadata.Value, metricMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

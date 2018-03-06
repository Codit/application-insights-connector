using System.Net;
using System.Web.Http;
using Codit.Connectors.ApplicationInsights.Contracts.v1;
using Codit.Connectors.ApplicationInsights.Exceptions;
using Codit.Connectors.ApplicationInsights.Filters;
using Swashbuckle.Swagger.Annotations;

namespace Codit.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    [SharedAccessKeyAuthentication]
    public class MetricsController : ApiController
    {
        /// <summary>
        ///     Tracks a custom metric to Azure Application Insights
        /// </summary>
        /// <param name="metricMetadata">Metadata concerning the metric to track</param>
        [HttpPost]
        [Route("metrics")]
        [SwaggerOperation("metrics")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Metric was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Specified metric metadata was invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "We were unable to succesfully process the request")]
        public IHttpActionResult Metric([FromBody] BasicMetricMetadata metricMetadata)
        {
            if (metricMetadata == null)
            {
                return BadRequest("No metadata about the metric was specified");
            }
            if (string.IsNullOrWhiteSpace(metricMetadata.Name))
            {
                return BadRequest("No metric name was specified");
            }

            try
            {
                var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(metricMetadata.InstrumentationKey);
                applicationInsightsTelemetry.TrackMetric(metricMetadata.Name, metricMetadata.Value, metricMetadata.CustomProperties);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (InstrumentationKeyNotSpecifiedException)
            {
                return Content(HttpStatusCode.InternalServerError, Constants.Errors.MissingInstrumentationKey);
            }
        }

        /// <summary>
        ///     Tracks a custom metric with sampling to Azure Application Insights
        /// </summary>
        /// <param name="metricMetadata">Metadata concerning the metric to track</param>
        [HttpPost]
        [Route("metrics/sampling")]
        [SwaggerOperation("metrics/sampling")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Metric was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Specified metric metadata was invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "We were unable to succesfully process the request")]
        public IHttpActionResult Metric([FromBody] SampledMetricMetadata metricMetadata)
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

            try
            {
                var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(metricMetadata.InstrumentationKey);
                applicationInsightsTelemetry.TrackSampledMetric(metricMetadata.Name, metricMetadata.Sum.Value, metricMetadata.Count, metricMetadata.Max, metricMetadata.Min, metricMetadata.StandardDeviation, metricMetadata.CustomProperties);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (InstrumentationKeyNotSpecifiedException)
            {
                return Content(HttpStatusCode.InternalServerError, Constants.Errors.MissingInstrumentationKey);
            }
        }
    }
}
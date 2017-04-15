using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TomKerkhove.Connectors.ApplicationInsights.Contracts.v1;

namespace TomKerkhove.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    public class TelemetryController : ApiController
    {
        /// <summary>
        /// Writes a trace to Azure Application Insights
        /// </summary>
        /// <param name="traceMetadata">Description about the requested trace</param>
        [HttpPost]
        [Route("telemetry")]
        [SwaggerResponse(HttpStatusCode.NoContent, description: "Trace was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, description: "Specified trace metadata was invalid")]
        public IHttpActionResult Trace([FromBody]TraceMetadata traceMetadata)
        {
            if (traceMetadata == null)
            {
                return BadRequest("No trace metadata was specified");
            }
            if (string.IsNullOrWhiteSpace(traceMetadata.Message))
            {
                return BadRequest("No message was specified");
            }

            ApplicationInsightsTelemetry.Trace(traceMetadata.Message, traceMetadata.SeverityLevel, traceMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

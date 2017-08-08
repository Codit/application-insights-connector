using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Codit.Connectors.ApplicationInsights.Contracts.v1;

namespace Codit.Connectors.ApplicationInsights.Controllers
{
    /// <summary>
    /// Provides operations related to tracing to Azure Application Insights
    /// </summary>
    [RoutePrefix("api/v1")]
    public class TracesController : ApiController
    {
        /// <summary>
        /// Tracks a trace to Azure Application Insights
        /// </summary>
        /// <param name="traceMetadata">Metadata concerning the trace to track</param>
        [HttpPost]
        [Route("traces")]
        [SwaggerOperation("traces")]
        [SwaggerResponse(HttpStatusCode.NoContent, description: "Trace was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, description: "Specified trace metadata was invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, description:"We were unable to succesfully process the request")]
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

            var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(traceMetadata.InstrumentationKey);
            applicationInsightsTelemetry.TrackTrace(traceMetadata.Message, traceMetadata.SeverityLevel, traceMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

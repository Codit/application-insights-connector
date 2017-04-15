using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TomKerkhove.Connectors.ApplicationInsights.Contracts;

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
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public Task<IHttpActionResult> WriteTrace([FromBody]TraceMetadata traceMetadata)
        {
            if (traceMetadata == null)
            {
                return Task.FromResult((IHttpActionResult)BadRequest("No trace metadata was specified"));
            }
            if (string.IsNullOrWhiteSpace(traceMetadata.Message))
            {
                return Task.FromResult((IHttpActionResult)BadRequest("No message was specified"));
            }

            ApplicationInsightsTelemetry.Trace($"{traceMetadata.Message}", traceMetadata.CustomProperties);

            return Task.FromResult((IHttpActionResult)Ok());
        }
    }
}

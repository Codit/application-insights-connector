using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace TomKerkhove.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    public class EventsController : ApiController
    {
        /// <summary>
        /// Tracks a custom event to Azure Application Insights
        /// </summary>
        /// <param name="eventMetadata">Metadata concerning the event to track</param>
        [HttpPost]
        [Route("events")]
        [SwaggerResponse(HttpStatusCode.NoContent, description: "Trace was successfully written to Azure Application Insights")]
        [SwaggerResponse(HttpStatusCode.BadRequest, description: "Specified trace metadata was invalid")]
        public IHttpActionResult WriteEvent([FromBody]Contracts.v1.EventMetadata eventMetadata)
        {
            if (eventMetadata == null)
            {
                return BadRequest("No metadata about the event was specified");
            }
            if (string.IsNullOrWhiteSpace(eventMetadata.Name))
            {
                return BadRequest("No event name was specified");
            }

            var applicationInsightsTelemetry = new ApplicationInsightsTelemetry(eventMetadata.InstrumentationKey);
            applicationInsightsTelemetry.TrackEvent(eventMetadata.Name, eventMetadata.CustomProperties);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

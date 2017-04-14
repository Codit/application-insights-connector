using System.Threading.Tasks;
using System.Web.Http;

namespace TomKerkhove.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    public class TelemetryController : ApiController
    {
        [HttpPost]
        [Route("telemetry")]
        public async Task<IHttpActionResult> WriteTrace()
        {
            return Ok();
        }
    }
}

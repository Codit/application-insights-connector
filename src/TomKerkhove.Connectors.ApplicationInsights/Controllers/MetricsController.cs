using System.Threading.Tasks;
using System.Web.Http;

namespace TomKerkhove.Connectors.ApplicationInsights.Controllers
{
    [RoutePrefix("api/v1")]
    public class MetricsController : ApiController
    {
        [HttpPost]
        [Route("metics")]
        public Task<IHttpActionResult> WriteMetric(string metricName)
        {
            return Task.FromResult((IHttpActionResult)NotFound());
        }
    }
}

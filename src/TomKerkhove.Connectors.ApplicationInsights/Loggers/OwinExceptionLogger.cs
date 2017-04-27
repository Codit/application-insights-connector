using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace TomKerkhove.Connectors.ApplicationInsights.Loggers
{
    public class OwinExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace TomKerkhove.Connectors.ApplicationInsights.ExceptionHandling
{
    public class OwinExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return base.ShouldHandle(context);
        }
    }
}
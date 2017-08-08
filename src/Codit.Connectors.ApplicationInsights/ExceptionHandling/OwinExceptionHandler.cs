using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Codit.Connectors.ApplicationInsights.ExceptionHandling
{
    public class OwinExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, "The request could not be completed successfully, please try again.");
            return Task.FromResult(context.Result = new ResponseMessageResult(response));
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return base.ShouldHandle(context);
        }
    }
}
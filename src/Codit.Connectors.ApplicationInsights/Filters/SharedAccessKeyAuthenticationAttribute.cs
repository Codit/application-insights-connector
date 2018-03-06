using Codit.Connectors.ApplicationInsights.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Codit.Connectors.ApplicationInsights.Filters
{
    public class SharedAccessKeyAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!SharedAccessKeySettings.IsEnabled())
            {
                return Task.CompletedTask;
            }

            context.Request.Headers.TryGetValues(SharedAccessKeySettings.GetHeaderName(), out var requestHeaders);
            if (requestHeaders == null)
            {
                context.ErrorResult = new AuthenticationFailureResult();
                return Task.CompletedTask;
            }

            if (!SharedAccessKeySettings.AccessKeyPool().Contains($"|{requestHeaders.First()}|"))
            {
                context.ErrorResult = new AuthenticationFailureResult();
            }

            return Task.CompletedTask;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool AllowMultiple => false;
    }
}
using Codit.Connectors.ApplicationInsights.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Codit.Connectors.ApplicationInsights.Filters
{
    public class ClientKeyAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (!SharedAccessKeySettings.IsSharedAccessKeyEnabled())
                {
                    return;
                }

                context.Request.Headers.TryGetValues(SharedAccessKeySettings.SharedAccessKeyHeaderName(), out var requestHeaders);
                if (requestHeaders == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult();
                    return;
                }

                if (!SharedAccessKeySettings.AccessKeyPool().Contains($"|{requestHeaders.First()}|"))
                {
                    context.ErrorResult = new AuthenticationFailureResult();
                }
            });
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool AllowMultiple => false;
    }
}
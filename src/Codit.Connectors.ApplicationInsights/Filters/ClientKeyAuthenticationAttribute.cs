using Codit.Connectors.ApplicationInsights.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace Codit.Connectors.ApplicationInsights.Filters
{
    public class ClientKeyAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (!SharedAccessKeySettings.IsSharedAccessKeyEnabled()) return;

                IEnumerable<string> requestHeaders;
                context.Request.Headers.TryGetValues(SharedAccessKeySettings.SharedAccessKeyHeaderName(), out requestHeaders);
                if (requestHeaders == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult();
                    return;
                }
                if (!SharedAccessKeySettings.AccessKeyPool().Contains(String.Format("|{0}|", requestHeaders.First())))
                    context.ErrorResult = new AuthenticationFailureResult();
            });
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {

            });
        }

        public bool AllowMultiple
        {
            get { return false; }
        }

        private class AuthenticationFailureResult : IHttpActionResult
        {
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }
            private HttpResponseMessage Execute()
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}
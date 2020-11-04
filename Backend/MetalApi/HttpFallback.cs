using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Polly;

namespace MetalApi
{
    public class HttpFallback
    {
        public static IAsyncPolicy<HttpResponseMessage> FallbackPolicy =>
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .FallbackAsync(
                    FallbackAction,
                    (httpResponseMessage, context) => Task.CompletedTask);

        private static Task<HttpResponseMessage> FallbackAction(
            DelegateResult<HttpResponseMessage> responseToFailedRequest,
            Context context,
            CancellationToken cancellationToken)
        {
            var uri = responseToFailedRequest.Result.RequestMessage.RequestUri;
            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent($"The fallback executed, the original error was " +
                $"{responseToFailedRequest.Result.Content.ReadAsStringAsync()}")
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}

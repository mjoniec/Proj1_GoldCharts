using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetalApi.Providers;
using Microsoft.AspNetCore.WebUtilities;
using Model;
using Newtonsoft.Json;
using Polly;

namespace MetalApi
{
    public class HttpFallback
    {
        private readonly IMetalsPricesProvider _metalsPricesProvider;

        public HttpFallback(IServiceProvider serviceProvider)
        {
            _metalsPricesProvider = (IMetalsPricesProvider)serviceProvider.GetService(typeof(FallbackMetalsPricesProvider));
        }

        public IAsyncPolicy<HttpResponseMessage> FallbackPolicy =>
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .FallbackAsync(
                    FallbackAction,
                    (httpResponseMessage, context) => Task.CompletedTask);

        private Task<HttpResponseMessage> FallbackAction(
            DelegateResult<HttpResponseMessage> responseToFailedRequest,
            Context context,
            CancellationToken cancellationToken)
        {
            var uri = responseToFailedRequest.Result.RequestMessage.RequestUri;
            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value))
                .ToList()
                .ToArray();

            var metalPrices = _metalsPricesProvider.Get(
                (MetalType)Enum.Parse(typeof(MetalType), items[0].Value),
                DateTime.Parse(items[1].Value),
                DateTime.Parse(items[2].Value)).Result;

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(metalPrices), Encoding.UTF8, "application/json")
                //new StringContent($"The fallback executed, the original error was " +
                //    $"{responseToFailedRequest.Result.Content.ReadAsStringAsync()}")
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}

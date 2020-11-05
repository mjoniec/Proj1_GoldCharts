using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetalApi.Providers;
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
            var uri = responseToFailedRequest.Result.RequestMessage.RequestUri.ToString();
            var metalType = uri.Contains("GOLD") ? MetalType.Gold : MetalType.Silver;
            var metalPrices = _metalsPricesProvider.Get(metalType).Result;

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

using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonModel;
using MetalApi.Providers;
using Polly;

namespace MetalApi
{
    public class HttpFallback
    {
        private readonly IMetalProvider _metalsPricesProvider;

        public HttpFallback(IServiceProvider serviceProvider)
        {
            _metalsPricesProvider = (IMetalProvider)serviceProvider.GetService(typeof(FallbackMetalsPricesProvider));
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
            var metalType = uri.Contains("GOLD") ? Metal.Gold : Metal.Silver;
            var metalPrices = _metalsPricesProvider.Get(metalType).Result;

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent(metalPrices, Encoding.UTF8, "application/json")
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}

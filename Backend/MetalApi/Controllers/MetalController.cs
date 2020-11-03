using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Model;
using System;
using MetalsDataProvider.ReadModel;
using System.Net.Http;

namespace MetalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalController : ControllerBase
    {
        //TODO: make readonly and from json config
        //private const string GoldPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";
        //private const string SilverPricesUrl = "https://www.quandl.com/api/v3/datasets/LBMA/SILVER.json";
        private const string GoldPricesUrl = "WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "LBMA/SILVER.json";

        private readonly IHttpClientFactory _httpClientFactory;

        public MetalController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{currency}/{metal}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(MetalType metal, DateTime start, DateTime end)
        {


            var prices = await _combineCurrencyAndMetalDataService.GetMetalPricesInCurrency(currency, metal, start, end);

            if (prices == null)
            {
                return NoContent();
            }

            return Ok(prices);
        }

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("QuandlService");
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(GoldPricesUrl);

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;
            metalPrices.Currency = Currency.AUD;

            return metalPrices;
        }

        public async Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(SilverPricesUrl);

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;
            metalPrices.Currency = Currency.USD;

            return metalPrices;
        }
    }
}
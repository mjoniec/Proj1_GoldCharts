using MetalReadModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Model;
using System;
using System.Net.Http;
using MetalApi.GuandlModel;

namespace MetalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalController : ControllerBase
    {
        //TODO: make readonly and from json config
        private const string GoldPricesUrl = "WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "LBMA/SILVER.json";

        private readonly IHttpClientFactory _httpClientFactory;

        public MetalController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{metalType}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(MetalType metalType, DateTime start, DateTime end)
        {
            string json;

            if (metalType == MetalType.Gold)
            {
                json = await GetPrices(GoldPricesUrl);
            }
            else
            {
                json = await GetPrices(SilverPricesUrl);
            }

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;
            metalPrices.Currency = Currency.AUD;

            if (metalPrices == null)
            {
                return NoContent();
            }

            return Ok(metalPrices);
        }

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("QuandlService");//To config
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
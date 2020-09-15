using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GoldChartsApi.Services;
using System.Threading.Tasks;
using CurrencyDataProvider.ReadModel;
using MetalsDataProvider.ReadModel;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalsPricesController : ControllerBase
    {
        CombineCurrencyAndMetalDataService _combineCurrencyAndMetalDataService;

        public MetalsPricesController(CombineCurrencyAndMetalDataService combineCurrencyAndMetalDataService)
        {
            _combineCurrencyAndMetalDataService = combineCurrencyAndMetalDataService;
        }

        //https://localhost:44314/api/MetalsPrices/USD/Silver
        //https://localhost:44314/api/MetalsPrices/AUD/Gold
        [HttpGet("{currency}/{metal}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Currency currency, Metal metal)
        {
            var prices = await _combineCurrencyAndMetalDataService.GetMetalpricesInCurrency(currency, metal);

            if (prices == null)
            {
                return NoContent();
            }

            return Ok(prices);
        }
    }
}

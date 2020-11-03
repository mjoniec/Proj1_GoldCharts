using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GoldChartsApi.Services;
using System.Threading.Tasks;
using Model;
using System;

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

        //https://localhost:44314/api/MetalsPrices/USD/Silver/2000-1-1/2005-1-1
        //https://localhost:44314/api/MetalsPrices/AUD/Gold/2000-1-1/2005-1-1
        [HttpGet("{currency}/{metal}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Currency currency, MetalType metal, DateTime start, DateTime end)
        {
            var prices = await _combineCurrencyAndMetalDataService.GetMetalPricesInCurrency(currency, metal, start, end);

            if (prices == null)
            {
                return NoContent();
            }

            return Ok(prices);
        }
    }
}

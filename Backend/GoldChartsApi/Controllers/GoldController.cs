using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GoldChartsApi.Services;
using System.Threading.Tasks;
using CurrencyDataProvider.Model;
using GoldChartsApi.Model;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        CombineCurrencyAndMetalDataService _combineCurrencyAndMetalDataService;

        public GoldController(CombineCurrencyAndMetalDataService combineCurrencyAndMetalDataService)
        {
            _combineCurrencyAndMetalDataService = combineCurrencyAndMetalDataService;
        }

        // https://localhost:44314/api/Gold/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var prices = await _combineCurrencyAndMetalDataService.GetMetalpricesInCurrency(Currency.USD, Metal.Gold);

            if (prices == null)
            {
                return NoContent();
            }

            return Ok(prices);
        }

        // GET: GoldPrices/Prices
        //[HttpGet("[action]")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> GetPrices()
        //{
        //    var dailyGoldPrices = await _metalsPricesProvider.GetGoldPrices();

        //    if (dailyGoldPrices == null)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(dailyGoldPrices);
        //}

        //[HttpGet("[action]")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> GetCurrencies()
        //{
        //    var currencies = _currenciesExchangeDataRepository.GetExchangeRates(Currency.AUD, Currency.USD);

        //    if (currencies == null)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(currencies);
        //}
    }
}

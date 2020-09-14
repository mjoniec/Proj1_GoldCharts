using Microsoft.AspNetCore.Mvc;
using MetalsDataProvider.Providers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CurrencyDataProvider.Repositories;
using CurrencyDataProvider.Model;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        IMetalsPricesProvider _metalsPricesProvider;
        CurrenciesExchangeDataRepository _currenciesExchangeDataRepository;

        public GoldController(IMetalsPricesProvider metalsPricesProvider,
            CurrenciesExchangeDataRepository currenciesExchangeDataRepository)
        {
            _metalsPricesProvider = metalsPricesProvider;
            _currenciesExchangeDataRepository = currenciesExchangeDataRepository;
        }

        // GET: GoldPrices
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("GoldPricesController is working");
        }

        // GET: GoldPrices/Prices
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPrices()
        {
            var dailyGoldPrices = await _metalsPricesProvider.GetGoldPrices();

            if (dailyGoldPrices == null)
            {
                return NoContent();
            }

            return Ok(dailyGoldPrices);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = _currenciesExchangeDataRepository.GetExchangeRates(Currency.AUD, Currency.USD);

            if (currencies == null)
            {
                return NoContent();
            }

            return Ok(currencies);
        }
    }
}

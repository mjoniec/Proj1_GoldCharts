using System;
using System.Threading.Tasks;
using CommonReadModel;
using CurrencyDataProvider.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyProvider _currencyProvider;

        public CurrencyController(IServiceProvider serviceProvider)
        {
            //_currencyProvider = (ICurrencyProvider)serviceProvider.GetService(typeof(CurrencyRepository));
            _currencyProvider = (ICurrencyProvider)serviceProvider.GetService(typeof(CurrencyFallback));
        }

        //http://localhost:54782/api/currency/USD/EUR/2000-1-1/2005-1-1
        [HttpGet("{baseCurrency}/{rateCurrency}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            var currencyRates = _currencyProvider.GetExchangeRates(baseCurrency, rateCurrency, start, end);

            if (currencyRates == null)
            {
                return NoContent();
            }

            return Ok(currencyRates);
        }
    }
}

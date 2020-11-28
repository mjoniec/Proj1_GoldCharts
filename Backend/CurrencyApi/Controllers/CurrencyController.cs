using System;
using System.Threading.Tasks;
using CommonReadModel;
using CurrencyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        //http://localhost:54782/api/currency/USD/EUR/2000-1-1/2005-1-1
        [HttpGet("{baseCurrency}/{rateCurrency}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            var currencyRates = _currencyService.GetExchangeRates(baseCurrency, rateCurrency, start, end);

            if (currencyRates == null)
            {
                return NoContent();
            }

            return Ok(currencyRates);
        }
    }
}

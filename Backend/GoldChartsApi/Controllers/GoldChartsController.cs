using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using CommonModel;
using GoldChartsApi.Pipe;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldChartsController : ControllerBase
    {
        private readonly MetalCurrencyCombinatorPipe _metalCurrencyCombinatorPipe;

        public GoldChartsController(MetalCurrencyCombinatorPipe metalCurrencyCombinatorPipe)
        {
            _metalCurrencyCombinatorPipe = metalCurrencyCombinatorPipe;
        }

        //https://localhost:44314/api/GoldCharts/USD/Silver/2000-1-1/2005-1-1
        //https://localhost:44314/api/GoldCharts/AUD/Gold/2000-1-1/2005-1-1
        [HttpGet("{currency}/{metal}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //TODO: return model type declaration
        public async Task<IActionResult> Get(Currency currency, Metal metal, DateTime start, DateTime end)
        {
            var metalCurrencyCombined = await _metalCurrencyCombinatorPipe.GetMetalPricesInCurrency(currency, metal, start, end);

            if (metalCurrencyCombined == null || metalCurrencyCombined.MetalPrices == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, metalCurrencyCombined.OperationStatus.ToString());
            }

            return Ok(metalCurrencyCombined.MetalPrices);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Model;
using System;
using MetalApi.Providers;

namespace MetalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalController : ControllerBase
    {
        private readonly IMetalsPricesProvider _metalsPricesProvider;

        public MetalController(IMetalsPricesProvider metalsPricesProvider)
        {
            _metalsPricesProvider = metalsPricesProvider;
        }

        [HttpGet("{metalType}/{start}/{end}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(MetalType metalType, DateTime start, DateTime end)
        {
            var metalPrices = _metalsPricesProvider.Get(metalType, start, end);
            
            if (metalPrices == null)
            {
                return NoContent();
            }

            return Ok(metalPrices);
        }
    }
}
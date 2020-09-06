using System.Collections.Generic;
using MetalsDataProvider;
using CurrencyRepository;
using Microsoft.AspNetCore.Mvc;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        // GET: api/Gold
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Gold/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            var a = new CurrencyRepository.Class1();
            var b = new MetalsDataProvider.Class1();

            return "value";
        }

        // POST: api/Gold
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Gold/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

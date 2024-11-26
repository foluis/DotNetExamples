using Microsoft.AspNetCore.Mvc;
using Models;

namespace ExampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private static List<Provider> providers =
        [
            new() {
                Id = 1,
                Name = "Perrors el loco",
                ContactName = "Saul",
                Phone = "1234"
            },
            new () {
                Id = 2,
                Name = "Magico Mundo",
                ContactName = "Mario",
                Phone = "23425"
            },
            new () {
                Id = 3,
                Name = "Carpintero",
                ContactName = "Mario",
                Phone = "23425"
            }
        ];

        [HttpGet]
        public ActionResult<List<Provider>> GetProviders()
        {
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public ActionResult<Provider> GetProviderById(int id)
        {
            var provider = providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
                return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public ActionResult<Provider> AddProvider(Provider newProvider)
        {
            if (newProvider is null)
                return BadRequest();

            newProvider.Id = providers.Max(g => g.Id) + 1;
            providers.Add(newProvider);


            return CreatedAtAction(nameof(GetProviderById), new { id = newProvider.Id }, newProvider);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProvider(int id, Provider updatedprovider)
        {
            var provider = providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
                return NotFound();

            provider.Name = updatedprovider.Name;
            provider.ContactName = updatedprovider.ContactName;
            provider.Phone = updatedprovider.Phone;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProvider(int id)
        {
            var provider = providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
                return NotFound();

            providers.Remove(provider);
            return NoContent();
        }
    }
}

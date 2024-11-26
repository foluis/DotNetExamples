using ExampleApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ExampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController(AppDbContexts appContext) : ControllerBase
    {
        private readonly AppDbContexts _appContext = appContext;

        //Old way, instead of using "primary constructors"

        //private readonly AppDbContexts _appContext;
        //public ProviderController(AppDbContexts appContext)
        //{
        //    _appContext = appContext;
        //}

        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetProviders()
        {
            return Ok(await _appContext.Providers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProviderById(int id)
        {
            var provider = await _appContext.Providers.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult<Provider>> AddProvider(Provider newProvider)
        {
            if (newProvider is null)
                return BadRequest();
            
            _appContext.Providers.Add(newProvider);
            await _appContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProviderById), new { id = newProvider.Id }, newProvider);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, Provider updatedprovider)
        {
            var provider = await _appContext.Providers.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            provider.Name = updatedprovider.Name;
            provider.ContactName = updatedprovider.ContactName;
            provider.Phone = updatedprovider.Phone;

            await _appContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _appContext.Providers.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            _appContext.Providers.Remove(provider);

            await _appContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SQLServerDatabaseExample.Data;

namespace SQLServerDatabaseExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController(AppDbContexts _appContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetProviders()
        {
            return Ok(await _appContext.VideoGames.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProviderById(int id)
        {
            var provider = await _appContext.VideoGames.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult<Provider>> AddProvider(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest();

            _appContext.VideoGames.Add(newGame);
            await _appContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProviderById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, VideoGame updatedGame)
        {
            if (id != updatedGame.Id)
                return BadRequest();

            var provider = await _appContext.VideoGames.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            provider.Title = updatedGame.Title;
            provider.Platform = updatedGame.Platform;

            await _appContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _appContext.VideoGames.FirstOrDefaultAsync(p => p.Id == id);
            if (provider is null)
                return NotFound();

            _appContext.VideoGames.Remove(provider);

            await _appContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
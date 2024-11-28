using InMemoryExample.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace InMemoryExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly AppDbContexts _context;

        public VideoGameController(AppDbContexts context)
        {
            _context = context;
        }

        // GET: api/VideoGame
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGame>>> GetVideoGames()
        {
            return await _context.VideoGames.ToListAsync();
        }

        // GET: api/VideoGame/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGame(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);

            if (videoGame == null)
            {
                return NotFound();
            }

            return videoGame;
        }

        // PUT: api/VideoGame/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoGame(int id, VideoGame videoGame)
        {
            if (id != videoGame.Id)
            {
                return BadRequest();
            }

            _context.Entry(videoGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoGameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VideoGame
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VideoGame>> PostVideoGame(VideoGame videoGame)
        {
            _context.VideoGames.Add(videoGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideoGame", new { id = videoGame.Id }, videoGame);
        }

        // DELETE: api/VideoGame/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }

            _context.VideoGames.Remove(videoGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoGameExists(int id)
        {
            return _context.VideoGames.Any(e => e.Id == id);
        }
    }
}
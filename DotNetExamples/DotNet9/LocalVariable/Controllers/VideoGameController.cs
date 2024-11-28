using Microsoft.AspNetCore.Mvc;
using Models;

namespace LocalVariable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new ()
            {
                Id= 1,
                Name = "Mario Bros",
                Description = "PS5"
            },
            new ()
            {
                Id= 2,
                Name = "Ninja Tortules",
                Description = "Nintendo"
            }
        };

        [HttpGet]
        public ActionResult<List<Provider>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        [HttpGet("{id}")]
        public ActionResult<Provider> GetVideoGameById(int id)
        {  
            var game = videoGames.FirstOrDefault(p => p.Id == id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public ActionResult<Provider> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest();

            newGame.Id = videoGames.Max(g => g.Id) + 1;
            videoGames.Add(newGame);

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game = videoGames.FirstOrDefault(p => p.Id == id);
            if (game is null)
                return NotFound();

            game.Name = updatedGame.Name;
            game.Description = updatedGame.Description;            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(p => p.Id == id);
            if (game is null)
                return NotFound();

            videoGames.Remove(game);

            return NoContent();
        }
    }
}

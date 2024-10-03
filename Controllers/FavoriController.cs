using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerRaph.Models;

namespace TaskManagerRaph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly TaskContext _context;

        public FavoriteController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Favorite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavorites()
        {
            return await _context.Favorites.Include(f => f.Tasks).ToListAsync();
        }

        // GET: api/Favorite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favoris>> GetFavorite(int id)
        {
            var favorite = await _context.Favorites.Include(f => f.Tasks).FirstOrDefaultAsync(f => f.Id == id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // POST: api/Favorite
        [HttpPost]
        public async Task<ActionResult<Favoris>> PostFavorite(Favoris favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.Id }, favorite);
        }

        // PUT: api/Favorite/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, Favoris favorite)
        {
            if (id != favorite.Id)
            {
                return BadRequest();
            }

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // DELETE: api/Favorite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteExists(int id)
        {
            return _context.Favorites.Any(e => e.Id == id);
        }
    }
}

using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class postsController : ControllerBase
    {
        private readonly postsDBContext _context;

        public postsController(postsDBContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<posts>> Get()
            => await _context.post.ToListAsync();
        [HttpGet("id")]
        [ProducesResponseType(typeof(posts), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _context.post.FindAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(posts Post)
        {
            await _context.post.AddAsync(Post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Post.Id }, Post);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, posts Post)
        {
            if (id != Post.Id) return BadRequest();

            _context.Entry(Post).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var postsToDelete = await _context.post.FindAsync(id); 
            if (postsToDelete == null) return NotFound(); 

            _context.post.Remove(postsToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

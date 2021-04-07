using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBooks.Domain;
using ApiBooks.Services;
using ApiBooks.Utils;

namespace ApiBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly UserContext _context;
        private ArticlesService _articlesService;

        public ArticlesController(UserContext context, ArticlesService articlesService)
        {
            _context = context;
            _articlesService = articlesService;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<IEnumerable<Articles>> GetArticles()
        {
            //return await _context.Articles.ToListAsync();
            return await _articlesService.FindAll();
        }

        [HttpGet("paginator")]
        public async Task<IEnumerable<Articles>> GetArticlesPaginator([FromQuery] PaginatorFilter filter)
        {
            //return await _context.Articles.ToListAsync();
            return await _articlesService.FindAll(filter);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
       public async Task<ActionResult<Articles>> GetArticles(int id)
        {
            var articles = await _articlesService.FindByIdAsync(id);

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticles(int id, Articles articles)
        {
            if (id != articles.ArticlesId)
            {
                return BadRequest();
            }
            _context.Entry(articles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Articles>> PostArticles(Articles articles)
        {
            _context.Articles.Add(articles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticles", new { id = articles.ArticlesId }, articles);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticles(int id)
        {
            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(articles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticlesExists(int id)
        {
            return _context.Articles.Any(e => e.ArticlesId == id);
        }
    }
}

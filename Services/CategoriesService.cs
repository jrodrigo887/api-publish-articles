using ApiBooks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBooks.Services.Exception;

namespace ApiBooks.Services
{
    public class CategoriesService
    {
        private readonly UserContext _context;
        //private readonly ArticlesService _articlesService;

        public CategoriesService(UserContext context)
        {
            _context = context;
            // _articlesService = articlesService;
        }

        public async Task<List<Categories>> findAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<int> categoryCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<Categories> findByIdAsync(int id)
        {
            //var articles = await _articlesService.FindByIdAndCategoriesAsync(id);
            try
            {
                var category = await _context.Categories.FindAsync(id);

                return category;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException("Dados não encontrados.");
            }
            //user.Articles = articles.ToList();

            //return _context.Users.Find(id);
        }

        public async Task<dynamic> findByIdCategoriesWithArticlesAsync(int id)
        {
            //var articles = await _articlesService.FindByIdAndCategoriesAsync(id);
            try
            {
                var category = await _context.Categories.FindAsync(id);
                var articles = await _context.Articles
                    .Where(a => a.CategoriesId == category.CategoriesId)
                    .ToListAsync();
                return new 
                {
                    data = category,
                    articles = articles
                };
            }
            catch (NotFoundException)
            {
                throw new NotFoundException("Dados não encontrados.");
            }
        }

        public async Task InsertAsync(Categories obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categories cte)
        {
            var hasAny = await UserExists(cte.CategoriesId);

            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado.");
            }

            try
            {
                _context.Update(cte);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task<Categories> RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Categories.FindAsync(id);
                _context.Categories.Remove(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é permitido deletar categoria que está relacionado com artigos.");
            }
        }

        private async Task<bool> UserExists(int id)
        {
            return await _context.Categories.AnyAsync(x => x.CategoriesId == id);
        }
    }
}

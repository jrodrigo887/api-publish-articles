using ApiBooks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApiBooks.Utils;

namespace ApiBooks.Services
{
    public class ArticlesService
    {
        private readonly UserContext _context;

        public ArticlesService(UserContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Articles>> FindAll()
        {
            return await _context.Articles.OrderBy(n => n.Name).ToListAsync();
        }  

        public async Task<IEnumerable<Articles>> FindAll(PaginatorFilter filter)
        {
            return await _context.Articles.OrderBy(n => n.Name)
                .Skip((filter.pageNumber - 1) * filter.pageSize)
                .Take(filter.pageSize)
                .ToListAsync();
        }

        public async Task<int> articlesCountAsync()
        {
            return await _context.Articles.CountAsync();
        }

        public async Task<Articles> FindByIdAsync(int id)
        {
            return await _context.Articles
                .Include(article => article.Categories)
                .FirstOrDefaultAsync(a => a.ArticlesId == id);
        }

        // Retorna os artigos com as categorias.
        public async Task<IEnumerable<Articles>> FindByIdAndCategoriesAsync(int id)
        {
            try
            {
                var data = await _context.Articles
                    .Where(a => a.UserId == id)
                    .Include(article => article.Categories)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
                return data;
            }
            catch (ApplicationException)
            {
                throw new NotFoundException("Artigos não encontrado.");
            }
        }

        public void Remove(int id)
        {
            var obj = _context.Articles.Find(id);
            _context.Articles.Remove(obj);
            _context.SaveChanges();

        }
    }
}

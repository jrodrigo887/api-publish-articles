using ApiBooks.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiBooks.Enums;
using ApiBooks.Services.Exception;

namespace ApiBooks.Services
{
    public class UserService : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ArticlesService _articlesService;

        public UserService(UserContext context, ArticlesService articlesService)
        {
            _context = context;
            _articlesService = articlesService;
        }


        public async Task<List<User>> findAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> userCountAsync()
        {
            return await _context.Users.CountAsync() ;
        }


        public async Task<User> findByIdAsync(int id)
        {
            var hasExist = await UserExists(id);
            Console.WriteLine(hasExist);
            try
            {
                if (!hasExist)
                {
                    throw new NotFoundException("Usuário não Encontrado.");
                }
                var user = await _context.Users.FindAsync(id);
                var articles = await _articlesService.FindByIdAndCategoriesAsync(id);
                user.Articles = articles.ToList();
                return user;
            }
            catch (ApplicationException)
            {
                throw new NotFoundException("Usuário não Encontrado.");
            }

            //return _context.Users.Find(id);
        }

        public async Task InsertAsync(User obj)
        {
            //var HasEmail = await EmailExists(obj);

            //if (HasEmail)
            // {
            //  throw new NotImplementException("Email já cadastrado");
            //}
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();

            }
            catch(NotImplementException)
            {
                throw new NotImplementException("Email existente, tente um novo email.");
            }

           // return NoContent();
        }

        public async Task UpdateAsync(User user)
        {
            var hasAny = await UserExists(user.UserId);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found.");
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }



        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(x => x.UserId == id);
        }

        private async Task<bool> EmailExists(User user)
        {
            return await _context.Users.AnyAsync(x => x.Email == user.Email);
        }



    }
}

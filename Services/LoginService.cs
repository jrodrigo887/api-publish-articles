using ApiBooks.Domain;
using ApiBooks.Services.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiBooks.Services
{
    public class LoginService
    {
        private readonly UserService userService;


        public LoginService(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> Get(string email, string password)
        {
            var users = new List<User>();
            //users.Add(new UserBase("Joao", "joao@gmail.com", "123", false, "administrador"));
            //users.Add(new UserBase("jose", "jose@gmail.com", "123", false, "gerente"));
            //users.Add(new UserBase("urias", "urias@gmail.com", "123", false, "funcionario"));
            //users.Add(new UserBase("Joao", "joao@gmail.com", "123", false, "administrador"));

            users.AddRange(await userService.findAllAsync());

            if (users != null )
            {
                return users.Where(x =>
                x.Email.ToLower() == email.ToLower() &&
                x.Password.ToLower() == password.ToLower())
                .FirstOrDefault();
            }

            throw new NotImplementedException();

        }
    }
}

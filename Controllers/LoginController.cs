using ApiBooks.Domain;
using ApiBooks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Controllers
{
    [Route("api/login")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;
        private readonly UserService userService;

        public LoginController(LoginService loginService, UserService userService)
        {
            this.loginService = loginService;
            this.userService = userService;
        }


        [HttpPost]
        public async Task<ActionResult<dynamic>> Authentication([FromBody] User user)
        {

            var us = await loginService.Get(user.Email, user.Password);

            if (us == null)
                return NotFound(new { message = "Usuário ou senha inválida." });

            var token = TokenService.GenerateToken(us);
            us.Password = "";

            return new
            {
                user = us,
                token = token
            };
        }

        //[Route("create")]
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<dynamic>> Create([FromBody] User user)
        {

            if (user == null)
                return NotFound(new { message = "Informe os dados necessários." });

            await userService.InsertAsync(user);
            return new
            {
                message = "Usuário criado com sucesso!.",
                Name = user.Name,
                Email = user.Email,
                Cargo = user.Roles
            };

        }
    }
}

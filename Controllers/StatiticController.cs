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
    [Route("api/stat")]
    [ApiController]
    [AllowAnonymous]
    public class StaticController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly ArticlesService _articlesService;
        private readonly CategoriesService _categoriesService;

        public StaticController(UserService userService, ArticlesService articlesService, CategoriesService categoriesService)
        {
            this._articlesService = articlesService;
            this._categoriesService = categoriesService;
            this._userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<dynamic>> Static()
        {
            try
            {

                var countUser = await _userService.userCountAsync();
                var countCategories = await _categoriesService.categoryCountAsync();
                var countArticles = await _articlesService.articlesCountAsync();

                return new
                {
                    users = countUser,
                    categories = countCategories,
                    articles = countArticles,
                };
            }
            catch (ApplicationException e)
            {
                throw new NotFoundException(e.Message);
            }

           
        }
    }
}

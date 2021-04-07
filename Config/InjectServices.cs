using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBooks.Domai.Seeds;
using ApiBooks.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ApiBooks.Config
{
    public static class InjectServices
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SeedDatas>();
            services.AddScoped<UserService>();
            services.AddScoped<ArticlesService>();
            services.AddScoped<CategoriesService>();
            services.AddScoped<LoginService>();

            return services;
        }
    }
}

using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBooks.Domain;

namespace ApiBooks.Services
{
    public static class TokenService
    {
        public static string GenerateToken(UserBase user)
        {
            var tokeHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Roles.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature )
            };

            var token = tokeHandler.CreateToken(tokenDescriptor);

            return tokeHandler.WriteToken(token);
        }
    }
}
 
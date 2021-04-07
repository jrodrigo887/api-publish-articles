
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiBooks.Domain
{
    public abstract class UserBase
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        //[Remote(action: "VerifyEmail", controller: "UserBase")]
        public string Email { get; set; }
        [Required]
        public string Roles { get; set; }

        [Required]
        public string Password { get; set; }

        protected UserBase()
        {
        }

        public UserBase(string name, string email, string password, string role)
        {
            Roles = role;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}


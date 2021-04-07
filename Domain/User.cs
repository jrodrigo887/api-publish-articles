

using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiBooks.Domain
{
    public class User : UserBase
    {
        [JsonInclude]
        public ICollection<Articles> Articles { get; set; }

        public User()
        {
        }

        public User(string name, string email, string password, string role) : base(name, email,  password, role)
        {
        }
    }


}

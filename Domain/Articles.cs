using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBooks.Domain
{
    public class Articles   
    {
        public int ArticlesId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        [Required]
        public string Content { get; set; }

        public int? UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }


        public int? CategoriesId { get; set; }
        [JsonInclude]
        public Categories Categories { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Inserted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdated { get; set; }

        public Articles()
        {
        }

        public Articles(string name, string description, string imgUrl, string content)
        {
            Name = name;
            Description = description;
            ImgUrl = imgUrl;
            Content = content;
        }
    }
}

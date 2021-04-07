using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ApiBooks.Domain
{
    public class Categories
    {
        public int CategoriesId { get; set; }

        [Required(ErrorMessage ="Nome da categoria é obrigátorio!")]
        [StringLength(maximumLength:50, ErrorMessage = "Deve conter entre {2} a {1} caracter", MinimumLength = 3)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        //public int ArticlesId { get; set; }

        [JsonIgnore]
        public Articles Articles { get; set; }

        public Categories()
        {

        }

        public Categories(string name, int parent)
        {
            Name = name;
            ParentId = parent;
        }

        
    }
}

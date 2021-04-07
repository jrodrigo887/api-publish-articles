using System;
using System.Collections.Generic;
using System.Linq;


namespace ApiBooks.Domain.DTOs
{
    public class ArticlesDTO
    {
        public int ArticlesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public Categories Categories { get; set; }
    }
}

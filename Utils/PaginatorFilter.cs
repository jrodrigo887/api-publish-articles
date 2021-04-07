using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Utils
{
    public class PaginatorFilter
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public PaginatorFilter()
        {
            this.pageNumber = 1;
            this.pageSize = 10;
        }  
        public PaginatorFilter(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.pageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}

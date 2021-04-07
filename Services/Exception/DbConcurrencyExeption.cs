using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Services.Exception
{
    public class DbConcurrencyExeption: ApplicationException
    {
        DbConcurrencyExeption(string message) : base(message)
        {

        }
    }
}

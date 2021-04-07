using System;

namespace ApiBooks.Services.Exception
{
    public class NullException : ApplicationException
    {
        NullException(string message) : base(message)
        {

        }
    }
}

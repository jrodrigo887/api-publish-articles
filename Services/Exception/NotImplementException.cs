using System;


namespace ApiBooks.Services.Exception
{
    public class NotImplementException: ApplicationException
    {
        public NotImplementException(string message): base(message)
        {

        }
    }
}

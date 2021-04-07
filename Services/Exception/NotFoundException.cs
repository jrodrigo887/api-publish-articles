using System;


namespace ApiBooks.Services
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}

using System;

namespace Framework.Excepciones
{
    public class NotFoundedException : Exception
    {
        public NotFoundedException(string message) : base(message)
        {
        }
    }

    
}
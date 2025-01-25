namespace Hyperar.HUM.Application.Exceptions
{
    using System;

    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
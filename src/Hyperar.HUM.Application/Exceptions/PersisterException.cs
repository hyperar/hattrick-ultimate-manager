namespace Hyperar.HUM.Application.Exceptions
{
    using System;

    public class PersisterException : Exception
    {
        public PersisterException(string? message) : base(message)
        {
        }
    }
}
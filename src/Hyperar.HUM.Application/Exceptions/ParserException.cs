﻿namespace Hyperar.HUM.Application.Exceptions
{
    using System;

    public class ParserException : Exception
    {
        public ParserException(string? message) : base(message)
        {
        }
    }
}
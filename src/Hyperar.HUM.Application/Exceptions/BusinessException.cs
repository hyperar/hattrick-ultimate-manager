﻿namespace Hyperar.HUM.Application.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}

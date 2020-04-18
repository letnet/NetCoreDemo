using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Utility
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {

        }
    }
}

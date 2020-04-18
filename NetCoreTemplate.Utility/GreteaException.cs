using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTemplate.Utility
{
    public class GreteaException : Exception
    {
        public GreteaException(string message) : base(message)
        {

        }
    }
}

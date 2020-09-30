using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.Exceptions
{
    class MinimumLargerThanMaximumException : Exception
    {
        public MinimumLargerThanMaximumException()
            : base("Minimum cannot be larger than maximum") { }
    }
}

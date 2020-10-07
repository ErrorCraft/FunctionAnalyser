using System;

namespace CommandVerifier.Exceptions
{
    class MinimumLargerThanMaximumException : Exception
    {
        public MinimumLargerThanMaximumException()
            : base("Minimum cannot be larger than maximum") { }
    }
}

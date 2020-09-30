using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.NbtParser.Types
{
    interface NbtCollection : NbtArgument
    {
        public bool TryAdd(NbtArgument value);
    }
}

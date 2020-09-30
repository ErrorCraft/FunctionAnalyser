using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.ComponentParser.Types
{
    interface IComponent
    {
        bool Validate(StringReader reader, int start, bool mayThrow);
    }
}

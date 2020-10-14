using System;
using System.Collections.Generic;
using System.Text;
using CommandVerifier.ComponentParser;
using CommandVerifier.ComponentParser.JsonTypes;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Component : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            int start = reader.Cursor;
            if (!ComponentReader.TryRead(reader, throw_on_fail, out IComponent result)) return false;

            if (typeof(Null) == result.GetType())
            {
                reader.SetCursor(start);
                if (throw_on_fail) ComponentErrors.EmptyComponentError().AddWithContext(reader);
                return false;
            }
            if (!result.Validate(reader, start, throw_on_fail)) return false;
            SetLoopAttributes(reader);
            return true;
        }
    }
}

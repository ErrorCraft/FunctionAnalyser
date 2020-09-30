using System.Collections.Generic;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Colour : Subcommand
    {
        public static readonly HashSet<string> COLOURS = new HashSet<string>() { "aqua", "black", "blue", "dark_aqua", "dark_blue", "dark_gray", "dark_green", "dark_purple", "dark_red", "gold", "gray", "green", "light_purple", "red", "reset", "white", "yellow" };

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            if (!reader.TryReadUnquotedString(out string result)) return false;
            if (!COLOURS.Contains(result))
            {
                if (throw_on_fail) CommandError.UnknownColour(result).AddWithContext(reader);
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}

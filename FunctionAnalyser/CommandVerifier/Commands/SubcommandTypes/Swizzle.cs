using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Swizzle : Subcommand
    {
        [JsonProperty("characters", Required = Required.Always)]
        public HashSet<char> Characters { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            int start = reader.Cursor;
            HashSet<char> readValues = new HashSet<char>();

            while (!reader.IsEndOfArgument())
            {
                char c = reader.Read();
                if (!Characters.Contains(c) || readValues.Contains(c))
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.InvalidSwizzle(Characters).AddWithContext(reader);
                    return false;
                }
                readValues.Add(c);
            }
            SetLoopAttributes(reader);
            return true;
        }
    }
}

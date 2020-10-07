using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class NbtTag : Subcommand
    {
        [JsonProperty("forced_compound")]
        [DefaultValue(false)]
        public bool ForcedCompound { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            if (ForcedCompound)
            {
                if (!NbtParser.NbtReader.TryParse(reader, throw_on_fail, out _)) return false;
                reader.Information.NbtAccess++;
            }
            else
            {
                if (reader.CanRead())
                {
                    switch (reader.Peek())
                    {
                        case '{':
                            if (!NbtParser.NbtReader.TryReadCompound(reader, throw_on_fail, out _)) return false;
                            break;
                        case '[':
                            if (!NbtParser.NbtReader.TryReadArray(reader, throw_on_fail, out _)) return false;
                            break;
                        default:
                            if (!NbtParser.NbtReader.TryReadValue(reader, throw_on_fail, out _)) return false;
                            break;
                    }
                } else
                {
                    if (throw_on_fail) CommandError.ExpectedValue().AddWithContext(reader);
                    return false;
                }
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}

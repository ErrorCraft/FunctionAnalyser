using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class String : Subcommand
    {
        public enum StringType
        {
            [EnumMember(Value = "word")]
            Word = 0,
            [EnumMember(Value = "greedy")]
            Greedy = 1,
            [EnumMember(Value = "quotable")]
            Quotable = 2
        }

        [JsonProperty("string_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StringType Type { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead())
            {
                if (Optional)
                {
                    reader.Data.EndedOptional = true;
                    return true;
                }

                if (throw_on_fail) CommandError.IncorrectArgument().AddWithContext(reader);
                return false;
            }

            if (Type == StringType.Greedy)
            {
                reader.ReadRemaining();
                SetLoopAttributes(reader);
                return true;
            } else if (Type == StringType.Word)
            {
                if (!reader.TryReadUnquotedString(out _)) return false;
                SetLoopAttributes(reader);
                return true;
            } else
            {
                if (reader.TryReadString(throw_on_fail, out _)) return false;
                SetLoopAttributes(reader);
                return true;
            }
        }
    }
}

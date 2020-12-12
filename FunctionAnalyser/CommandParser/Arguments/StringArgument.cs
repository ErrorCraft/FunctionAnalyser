using CommandParser.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Arguments
{
    public class StringArgument : IArgument<string>
    {
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        private readonly StringType Type;

        public StringArgument(StringType type = StringType.WORD)
        {
            Type = type;
        }

        public ReadResults Parse(IStringReader reader, out string result)
        {
            if (Type == StringType.GREEDY)
            {
                result = reader.GetRemaining();
                reader.SetCursor(reader.GetLength());
                return new ReadResults(true, null);
            } else if (Type == StringType.WORD)
            {
                return reader.ReadUnquotedString(out result);
            } else
            {
                return reader.ReadString(out result);
            }
        }

        public enum StringType
        {
            [EnumMember(Value = "word")]
            WORD = 0,
            [EnumMember(Value = "phrase")]
            QUOTABLE = 1,
            [EnumMember(Value = "greedy")]
            GREEDY = 2
        }
    }
}

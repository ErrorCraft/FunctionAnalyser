using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Text : Subcommand
    {
        public enum TextType
        {
            [EnumMember(Value = "word")]
            Word = 0,
            [EnumMember(Value = "multi_word")]
            MultiWord = 1,
            [EnumMember(Value = "quoted")]
            Quoted = 2
        }

        [JsonProperty("text_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextType textType { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }
            switch (textType)
            {
                case TextType.Word:
                    if (!reader.CanRead())
                    {
                        if (throw_on_fail) CommandError.ExpectedValue().AddWithContext(reader);
                        return false;
                    }
                    if (reader.TryReadUnquotedString(out _))
                    {
                        SetLoopAttributes(reader);
                        return true;
                    }
                    return false;
                case TextType.MultiWord:
                    if (!reader.CanRead())
                    {
                        if (throw_on_fail) CommandError.IncorrectArgument().AddWithContext(reader);
                        return false;
                    }
                    reader.ReadRemaining();
                    SetLoopAttributes(reader);
                    return true;
                case TextType.Quoted:
                    if (reader.TryReadQuotedString(throw_on_fail, out _))
                    {
                        SetLoopAttributes(reader);
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}

using CommandVerifier.Commands.Converters;
using CommandVerifier.Commands.SubcommandTypes;
using CommandVerifier.Commands.SubcommandTypes.Selector;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.Collections
{
    public class EntitySelectorOption
    {
        public enum OptionPredicate
        {
            [EnumMember(Value = "none")]
            None = 0,
            [EnumMember(Value = "set_limit")]
            SetLimit = 1,
            [EnumMember(Value = "set_executor")]
            SetExecutor = 2
        }

        [JsonProperty("encapsulated")]
        [DefaultValue(false)]
        public bool Encapsulated { get; set; }

        [JsonProperty("allow_inverse")]
        [DefaultValue(false)]
        public bool AllowInverse { get; set; }

        [JsonProperty("predicate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(OptionPredicate.None)]
        public OptionPredicate Predicate { get; set; }

        [JsonProperty("components")]
        [JsonConverter(typeof(SubcommandConverter))]
        public Subcommand[] Components { get; set; }

        private static readonly Types.NamespacedId PLAYER_ENTITY = new Types.NamespacedId("player", false);

        public bool Handle(StringReader reader, bool mayThrow, EntitySelector entitySelector)
        {
            bool negated = false;
            if (AllowInverse)
            {
                if (reader.CanRead() && reader.Peek() == '!')
                {
                    negated = true;
                    reader.Skip();
                }
            }

            if (Predicate != OptionPredicate.None)
            {
                return Predicate switch
                {
                    OptionPredicate.SetLimit => TrySetEntityLimit(reader, mayThrow, entitySelector),
                    OptionPredicate.SetExecutor => TrySetExecutor(reader, mayThrow, entitySelector, negated),
                    _ => false,
                };
            }

            if (Encapsulated)
            {
                if (!reader.Expect('{', mayThrow)) return false;
                while (reader.CanRead() && reader.Peek() != '}')
                {
                    if (CheckComponents(reader, mayThrow))
                    {
                        reader.SkipWhitespace();
                        if (reader.CanRead())
                        {
                            char c = reader.Read();
                            if (c == ',') continue;
                            if (c == '}') return true;
                        }
                    }
                    else return false;
                }

                reader.SkipWhitespace();
                return reader.Expect('}', mayThrow);
            }
            else return CheckComponents(reader, mayThrow);
        }

        private bool CheckComponents(StringReader reader, bool mayThrow)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                reader.SkipWhitespace();
                if (!Components[i].Check(reader, mayThrow)) return false;
            }
            return true;
        }

        private bool TrySetEntityLimit(StringReader reader, bool mayThrow, EntitySelector entitySelector)
        {
            if (!reader.TryReadInt(mayThrow, out int result)) return false;

            if (result < 1)
            {
                if (mayThrow) CommandError.SelectorLimitTooLow().AddWithContext(reader);
                return false;
            }
            entitySelector.MaxResults = result;
            return true;
        }

        private bool TrySetExecutor(StringReader reader, bool mayThrow, EntitySelector entitySelector, bool negated)
        {
            if (!reader.TryReadNamespacedId(mayThrow, false, out Types.NamespacedId result)) return false;
            if (PLAYER_ENTITY.Equals(result) && !negated) entitySelector.IncludesEntities = false;
            return true;
        }
    }
}

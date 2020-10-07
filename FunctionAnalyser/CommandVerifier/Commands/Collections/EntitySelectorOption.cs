using CommandVerifier.Converters;
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
        public enum PredicateOption
        {
            [EnumMember(Value = "none")]
            None = 0,
            [EnumMember(Value = "set_limit")]
            SetLimit = 1,
            [EnumMember(Value = "set_executor")]
            SetExecutor = 2,
            [EnumMember(Value = "advancements")]
            Advancements = 3,
            [EnumMember(Value = "scores")]
            Scores = 4
        }

        [JsonProperty("allow_inverse")]
        [DefaultValue(false)]
        public bool AllowInverse { get; set; }

        [JsonProperty("predicate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(PredicateOption.None)]
        public PredicateOption Predicate { get; set; }

        [JsonProperty("components")]
        [JsonConverter(typeof(SubcommandConverter))]
        public Subcommand[] Components { get; set; }

        public bool Handle(StringReader reader, bool mayThrow, EntitySelector entitySelector)
        {
            bool negated = false;
            if (AllowInverse)
            {
                if (reader.CanRead() && reader.Peek() == '!')
                {
                    reader.Skip();
                    negated = true;
                }
            }

            reader.SkipWhitespace();
            return Predicate switch
            {
                PredicateOption.SetLimit => TrySetEntityLimit(reader, mayThrow, entitySelector),
                PredicateOption.SetExecutor => TrySetExecutor(reader, mayThrow, entitySelector, negated),
                PredicateOption.Advancements => TryReadAdvancements(reader, mayThrow),
                PredicateOption.Scores => TryReadScores(reader, mayThrow),
                _ => CheckComponents(reader, mayThrow)
            };
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
            
            if (Types.NamespacedId.PLAYER_ENTITY.Equals(result) && !negated) entitySelector.IncludesEntities = false;
            else if (!result.IsTag)
            {
                if (!result.IsDefaultNamespace() || !Entities.Options.Contains(result.Path))
                {
                    if (mayThrow) CommandError.UnknownEntity(result.ToString()).AddWithContext(reader);
                    return false;
                }
            }
            return true;
        }

        private bool TryReadAdvancements(StringReader reader, bool mayThrow)
        {
            if (!reader.Expect('{', mayThrow)) return false;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                if (!reader.TryReadNamespacedId(mayThrow, true, out _)) return false;

                reader.SkipWhitespace();
                if (!reader.Expect('=', mayThrow)) return false;

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == '{')
                {
                    reader.Skip();
                    reader.SkipWhitespace();
                    while (reader.CanRead() && reader.Peek() != '}')
                    {
                        reader.SkipWhitespace();
                        if (!reader.TryReadUnquotedString(out _)) return false;

                        reader.SkipWhitespace();
                        if (!reader.Expect('=', mayThrow)) return false;

                        reader.SkipWhitespace();
                        if (!reader.TryReadBoolean(mayThrow, out _)) return false;

                        reader.SkipWhitespace();
                        if (reader.CanRead())
                        {
                            char c = reader.Read();
                            if (c == ',') continue;
                            if (c == '}') break;
                        }
                    }
                }
                else if (!reader.TryReadBoolean(mayThrow, out _)) return false;

                reader.SkipWhitespace();
                if (reader.CanRead())
                {
                    char c = reader.Read();
                    if (c == ',') continue;
                    if (c == '}') return true;
                }

                if (mayThrow) CommandError.ExpectedCharacter('}').AddWithContext(reader);
                return false;
            }

            reader.SkipWhitespace();
            return reader.Expect('}', mayThrow);
        }

        private bool TryReadScores(StringReader reader, bool mayThrow)
        {
            if (!reader.Expect('{', mayThrow)) return false;

            IntegerRange integerRange = new IntegerRange() { Minimum = int.MinValue, Maximum = int.MaxValue };

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                if (!reader.TryReadUnquotedString(out _)) return false;

                reader.SkipWhitespace();
                if (!reader.Expect('=', mayThrow)) return false;

                reader.SkipWhitespace();
                if (!integerRange.Check(reader, mayThrow)) return false;

                reader.SkipWhitespace();
                if (reader.CanRead())
                {
                    char c = reader.Read();
                    if (c == ',') continue;
                    if (c == '}') return true;
                }

                if (mayThrow) CommandError.ExpectedCharacter('}').AddWithContext(reader);
                return false;
            }

            reader.SkipWhitespace();
            return reader.Expect('}', mayThrow);
        }
    }
}

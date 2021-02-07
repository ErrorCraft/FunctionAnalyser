using CommandParser.Context;
using CommandParser.Converters;
using CommandParser.Minecraft;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using CommandParser.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CommandParser.Collections
{
    enum Option
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
        Scores = 4,
        [EnumMember(Value = "gamemode")]
        Gamemode = 5,
        [EnumMember(Value = "sort")]
        Sort = 6
    }

    public enum ReapplicationType
    {
        [EnumMember(Value = "never")]
        Never = 0,
        [EnumMember(Value = "always")]
        Always = 1,
        [EnumMember(Value = "only_if_inverted")]
        OnlyIfInverted = 2
    }

    public class EntitySelectorOption
    {
        [JsonProperty("predicate"), JsonConverter(typeof(StringEnumConverter))]
        private readonly Option Option = Option.None;
        [JsonProperty("allow_inverse")]
        private readonly bool AllowInverse = false;
        [JsonProperty("reapplication_type"), JsonConverter(typeof(StringEnumConverter))]
        private readonly ReapplicationType ReapplicationType = ReapplicationType.Never;
        [JsonProperty("contents"), JsonConverter(typeof(NodeConverter))]
        private readonly ArgumentNode Contents = null;

        public ReapplicationType GetReapplicationType()
        {
            return ReapplicationType;
        }

        public ReadResults Handle(EntitySelectorParser parser, DispatcherResources resources, string name, int start, bool useBedrock)
        {
            IStringReader reader = parser.GetReader();
            bool negated = false;
            if (AllowInverse) negated = parser.ShouldInvertValue();

            ReadResults readResults = Option switch
            {
                Option.SetLimit => SetLimit(parser, reader),
                Option.SetExecutor => SetExecutor(parser, reader, negated, name, start, resources, useBedrock),
                Option.Advancements => ReadAdvancements(parser, reader),
                Option.Scores => ReadScores(parser, reader),
                Option.Gamemode => ReadGamemode(parser, reader, resources),
                Option.Sort => ReadSort(parser, reader, name, start, resources),
                _ => CheckComponents(parser, reader, resources)
            };

            if (readResults.Successful) parser.Apply(name, negated);
            return readResults;
        }

        private ReadResults CheckComponents(EntitySelectorParser parser, IStringReader reader, DispatcherResources resources)
        {
            if (Contents != null)
            {
                CommandContext context = new CommandContext(reader.GetCursor());
                ReadResults readResults = Contents.Parse(reader, context, resources);
                if (readResults.Successful)
                {
                    parser.AddArgument(context.Results[0]);
                }
                return readResults;
            }
            return new ReadResults(true, null);
        }

        private static ReadResults SetLimit(EntitySelectorParser parser, IStringReader reader)
        {
            ReadResults readResults = reader.ReadInteger(out int number);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (number < 1)
            {
                return new ReadResults(false, CommandError.SelectorLimitTooLow().WithContext(reader));
            }
            parser.SetMaxResults(number);
            parser.AddArgument(new ParsedArgument<int>(number, false));
            return new ReadResults(true, null);
        }

        private static ReadResults SetExecutor(EntitySelectorParser parser, IStringReader reader, bool negated, string originalName, int previousStart, DispatcherResources resources, bool useBedrock)
        {
            if (parser.IsTypeLimited())
            {
                reader.SetCursor(previousStart);
                return new ReadResults(false, CommandError.InapplicableOption(originalName).WithContext(reader));
            }

            int start = reader.GetCursor();

            bool isTag = false;
            if (reader.CanRead() && reader.Peek() == '#')
            {
                reader.Skip();
                isTag = true;
            }

            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation entity);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (ResourceLocation.PLAYER_ENTITY.Equals(entity) && !negated) parser.SetIncludesEntities(false);
            else if (useBedrock)
            {
                // Temporary
                return new ReadResults(true, null);
            }
            else if (!isTag)
            {
                if (!resources.Entities.Contains(entity))
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.InvalidEntityType(entity).WithContext(reader));
                }
            }
            parser.AddArgument(new ParsedArgument<Entity>(new Entity(entity, isTag), false));
            return new ReadResults(true, null);
        }

        private static ReadResults ReadAdvancements(EntitySelectorParser parser, IStringReader reader)
        {
            ReadResults readResults = reader.Expect('{');
            if (!readResults.Successful) return readResults;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                readResults = ResourceLocation.TryRead(reader, out ResourceLocation advancement);
                if (!readResults.Successful) return readResults;

                reader.SkipWhitespace();
                readResults = reader.Expect('=');
                if (!readResults.Successful) return readResults;

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == '{')
                {
                    reader.Skip();
                    reader.SkipWhitespace();
                    while (reader.CanRead() && reader.Peek() != '}')
                    {
                        reader.SkipWhitespace();
                        readResults = reader.ReadUnquotedString(out _);
                        if (!readResults.Successful) return readResults;

                        reader.SkipWhitespace();
                        readResults = reader.Expect('=');
                        if (!readResults.Successful) return readResults;

                        reader.SkipWhitespace();
                        readResults = reader.ReadBoolean(out _);
                        if (!readResults.Successful) return readResults;

                        reader.SkipWhitespace();
                        if (reader.CanRead())
                        {
                            char c = reader.Read();
                            if (c == ',') continue;
                            if (c == '}') break;
                        }
                    }
                }
                else
                {
                    readResults = reader.ReadBoolean(out _);
                    if (!readResults.Successful) return readResults;
                }

                parser.AddArgument(new ParsedArgument<Advancement>(new Advancement(advancement), false));
                reader.SkipWhitespace();
                if (reader.CanRead())
                {
                    char c = reader.Read();
                    if (c == ',') continue;
                    if (c == '}') return new ReadResults(true, null);
                }
                return new ReadResults(false, CommandError.ExpectedCharacter('}').WithContext(reader));
            }

            reader.SkipWhitespace();
            return reader.Expect('}');
        }

        private static ReadResults ReadScores(EntitySelectorParser parser, IStringReader reader)
        {
            ReadResults readResults = reader.Expect('{');
            if (!readResults.Successful) return readResults;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                readResults = reader.ReadUnquotedString(out _);
                if (!readResults.Successful) return readResults;

                reader.SkipWhitespace();
                readResults = reader.Expect('=');
                if (!readResults.Successful) return readResults;

                reader.SkipWhitespace();
                readResults = new RangeParser<int>(reader).Read(int.TryParse, CommandError.InvalidInteger, int.MinValue, int.MaxValue, false, out Range<int> range);
                if (!readResults.Successful) return readResults;

                parser.AddArgument(new ParsedArgument<Range<int>>(range, false));
                reader.SkipWhitespace();
                if (reader.CanRead())
                {
                    char c = reader.Read();
                    if (c == ',') continue;
                    if (c == '}') return new ReadResults(true, null);
                }
                return new ReadResults(false, CommandError.ExpectedCharacter('}').WithContext(reader));
            }

            reader.SkipWhitespace();
            return reader.Expect('}');
        }

        private static ReadResults ReadGamemode(EntitySelectorParser parser, IStringReader reader, DispatcherResources resources)
        {
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadUnquotedString(out string gamemode);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (resources.Gamemodes.TryGet(gamemode, out _))
            {
                parser.AddArgument(new ParsedArgument<Literal>(new Literal(gamemode), false));
                return new ReadResults(true, null);
            } else
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.UnknownGamemode(gamemode).WithContext(reader));
            }
        }

        private static ReadResults ReadSort(EntitySelectorParser parser, IStringReader reader, string originalName, int previousStart, DispatcherResources resources)
        {
            if (parser.IsSorted())
            {
                reader.SetCursor(previousStart);
                return new ReadResults(false, CommandError.InapplicableOption(originalName).WithContext(reader));
            }

            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadUnquotedString(out string sort);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (resources.Sorts.Contains(sort))
            {
                parser.AddArgument(new ParsedArgument<Literal>(new Literal(sort), false));
                return new ReadResults(true, null);
            }
            else
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.UnknownSort(sort).WithContext(reader));
            }
        }
    }
}

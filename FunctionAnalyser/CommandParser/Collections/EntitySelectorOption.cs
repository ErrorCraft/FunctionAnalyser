﻿using CommandParser.Context;
using CommandParser.Converters;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using CommandParser.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
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

        public ReadResults Handle(EntitySelectorParser parser, string name, int start)
        {
            StringReader reader = parser.GetReader();
            bool negated = false;
            if (AllowInverse) negated = parser.ShouldInvertValue();

            ReadResults readResults = Option switch
            {
                Option.SetLimit => SetLimit(parser, reader),
                Option.SetExecutor => SetExecutor(parser, reader, negated, name, start),
                Option.Advancements => ReadAdvancements(parser, reader),
                Option.Scores => ReadScores(parser, reader),
                Option.Gamemode => ReadGamemode(parser, reader),
                Option.Sort => ReadSort(parser, reader, name, start),
                _ => CheckComponents(parser, reader)
            };

            if (readResults.Successful) parser.Apply(name, negated);
            return readResults;
        }

        private ReadResults CheckComponents(EntitySelectorParser parser, StringReader reader)
        {
            if (Contents != null)
            {
                CommandContext context = new CommandContext(reader.Cursor);
                ReadResults readResults = Contents.Parse(reader, context);
                if (readResults.Successful)
                {
                    parser.AddArgument(context.Results[0]);
                }
                return readResults;
            }
            return new ReadResults(true, null);
        }

        private ReadResults SetLimit(EntitySelectorParser parser, StringReader reader)
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
            parser.AddArgument(new ParsedArgument<int>(number));
            return new ReadResults(true, null);
        }

        private ReadResults SetExecutor(EntitySelectorParser parser, StringReader reader, bool negated, string originalName, int previousStart)
        {
            if (parser.IsTypeLimited())
            {
                reader.Cursor = previousStart;
                return new ReadResults(false, CommandError.InapplicableOption(originalName).WithContext(reader));
            }

            int start = reader.Cursor;

            bool isTag = false;
            if (reader.CanRead() && reader.Peek() == ResourceLocation.TAG_CHARACTER)
            {
                reader.Skip();
                isTag = true;
            }

            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation entity);
            if (!readResults.Successful)
            {
                return readResults;
            }

            entity = new ResourceLocation(entity, isTag);

            if (ResourceLocation.PLAYER_ENTITY.Equals(entity) && !negated) parser.SetIncludesEntities(false);
            else if (!entity.IsTag)
            {
                if (!Entities.Contains(entity))
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.InvalidEntityType(entity).WithContext(reader));
                }
            }
            parser.AddArgument(new ParsedArgument<Entity>(new Entity(entity)));
            return new ReadResults(true, null);
        }

        private ReadResults ReadAdvancements(EntitySelectorParser parser, StringReader reader)
        {
            ReadResults readResults = reader.Expect('{');
            if (!readResults.Successful) return readResults;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                readResults = new ResourceLocationParser(reader).Read(out ResourceLocation advancement);
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

                parser.AddArgument(new ParsedArgument<Advancement>(new Advancement(advancement)));
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

        private ReadResults ReadScores(EntitySelectorParser parser, StringReader reader)
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

                parser.AddArgument(new ParsedArgument<Range<int>>(range));
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

        private ReadResults ReadGamemode(EntitySelectorParser parser, StringReader reader)
        {
            int start = reader.Cursor;
            ReadResults readResults = reader.ReadUnquotedString(out string gamemode);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (Gamemodes.Contains(gamemode))
            {
                parser.AddArgument(new ParsedArgument<Literal>(new Literal(gamemode)));
                return new ReadResults(true, null);
            } else
            {
                reader.Cursor = start;
                return new ReadResults(false, CommandError.UnknownGamemode(gamemode).WithContext(reader));
            }
        }

        private ReadResults ReadSort(EntitySelectorParser parser, StringReader reader, string originalName, int previousStart)
        {
            if (parser.IsSorted())
            {
                reader.Cursor = previousStart;
                return new ReadResults(false, CommandError.InapplicableOption(originalName).WithContext(reader));
            }

            int start = reader.Cursor;
            ReadResults readResults = reader.ReadUnquotedString(out string sort);
            if (!readResults.Successful)
            {
                return readResults;
            }

            if (Sorts.Contains(sort))
            {
                parser.AddArgument(new ParsedArgument<Literal>(new Literal(sort)));
                return new ReadResults(true, null);
            }
            else
            {
                reader.Cursor = start;
                return new ReadResults(false, CommandError.UnknownSort(sort).WithContext(reader));
            }
        }
    }
}
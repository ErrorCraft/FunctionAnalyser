using CommandParser.Collections;
using CommandParser.Context;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using System;
using System.Collections.Generic;
using Utilities;

namespace CommandParser.Parsers {
    public class EntitySelectorParser {
        private readonly IStringReader Reader;
        private readonly DispatcherResources Resources;
        private readonly int Start;
        private readonly bool UseBedrock;

        public IStringReader GetReader() {
            return Reader;
        }

        private bool IncludesEntities;
        private int MaxResults;
        private bool IsSelf;
        private SelectorType SelectorType;
        private readonly Dictionary<string, bool> AppliedOptions = new Dictionary<string, bool>();
        private readonly List<ParsedArgument> Arguments = new List<ParsedArgument>();
        private bool TypeLimited;
        private bool Sorted;

        public void SetMaxResults(int maxResults) {
            MaxResults = maxResults;
        }

        public void SetIncludesEntities(bool includesEntities) {
            IncludesEntities = includesEntities;
        }

        public bool IsTypeLimited() {
            return TypeLimited;
        }

        public bool IsSorted() {
            return Sorted;
        }


        public EntitySelectorParser(IStringReader reader, DispatcherResources resources, bool useBedrock) {
            Reader = reader;
            Start = reader.GetCursor();
            Resources = resources;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(out EntitySelector result) {
            result = default;
            ReadResults readResults;
            if (Reader.CanRead() && Reader.Peek() == '@') {
                Reader.Skip();
                readResults = ParseSelector();
            } else {
                readResults = ParseNameOrUuid();
            }

            if (readResults.Successful) {
                result = GetSelector();
            }

            return readResults;
        }

        private ReadResults ParseNameOrUuid() {
            ReadResults readResults = Reader.ReadString(out string s);
            if (!readResults.Successful) {
                return readResults;
            }

            if (UUID.TryParse(s, out _)) {
                IncludesEntities = true;
            } else {
                if (string.IsNullOrEmpty(s) || s.Length > 16) {
                    Reader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.InvalidNameOrUuid().WithContext(Reader));
                }
                IncludesEntities = false;
            }

            MaxResults = 1;
            SelectorType = SelectorType.None;
            return ReadResults.Success();
        }

        private ReadResults ParseSelector() {
            if (!Reader.CanRead()) {
                Reader.SetCursor(Start);
                return ReadResults.Failure(CommandError.MissingSelectorType().WithContext(Reader));
            }
            int currentPosition = Reader.GetCursor();
            char c = Reader.Read();
            switch (c) {
                case 'p':
                    MaxResults = 1;
                    IncludesEntities = false;
                    IsSelf = false;
                    SelectorType = SelectorType.NearestPlayer;
                    TypeLimited = true;
                    Sorted = false;
                    break;
                case 'a':
                    MaxResults = int.MaxValue;
                    IncludesEntities = false;
                    IsSelf = false;
                    SelectorType = SelectorType.AllPlayers;
                    TypeLimited = true;
                    Sorted = false;
                    break;
                case 'r':
                    MaxResults = 1;
                    IncludesEntities = false;
                    IsSelf = false;
                    SelectorType = SelectorType.RandomPlayer;
                    TypeLimited = true;
                    Sorted = false;
                    break;
                case 's':
                    MaxResults = 1;
                    IncludesEntities = false;
                    IsSelf = true;
                    SelectorType = SelectorType.Self;
                    TypeLimited = false;
                    Sorted = true;
                    break;
                case 'e':
                    MaxResults = int.MaxValue;
                    IncludesEntities = true;
                    IsSelf = false;
                    SelectorType = SelectorType.AllEntities;
                    TypeLimited = false;
                    Sorted = false;
                    break;
                default:
                    Reader.SetCursor(currentPosition);
                    return ReadResults.Failure(CommandError.UnknownSelectorType(c).WithContext(Reader));
            }
            if (UseBedrock) {
                currentPosition = Reader.GetCursor();
                Reader.SkipWhitespace();
                if (!Reader.CanRead() || Reader.Peek() != '[') Reader.SetCursor(currentPosition);
            }
            if (Reader.CanRead() && Reader.Peek() == '[') {
                Reader.Skip();
                ReadResults readResults = ParseOptions();
                if (!readResults.Successful) {
                    return readResults;
                }
            }
            return ReadResults.Success();
        }

        private bool MayApply(string name, EntitySelectorOption option) {
            if (AppliedOptions.TryGetValue(name, out bool inverted)) {
                ReapplicationType reapplicationType = option.GetReapplicationType();
                return reapplicationType switch {
                    ReapplicationType.Never => false,
                    ReapplicationType.Always => true,
                    ReapplicationType.OnlyIfInverted => inverted && WillBeInverted(),
                    _ => throw new InvalidOperationException()
                };
            } else {
                return true;
            }
        }

        private ReadResults ParseOptions() {
            Reader.SkipWhitespace();

            ReadResults readResults;

            while (Reader.CanRead() && Reader.Peek() != ']') {
                Reader.SkipWhitespace();
                int start = Reader.GetCursor();
                readResults = Reader.ReadString(out string name);
                if (!readResults.Successful) return readResults;
                if (!Resources.SelectorArguments.TryGet(name, out EntitySelectorOption option)) {
                    Reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.UnknownSelectorOption(name).WithContext(Reader));
                }

                Reader.SkipWhitespace();
                if (!Reader.CanRead() || Reader.Peek() != '=') {
                    return ReadResults.Failure(CommandError.ExpectedValueForSelectorOption(name).WithContext(Reader));
                }
                Reader.Skip();
                Reader.SkipWhitespace();

                if (!MayApply(name, option)) {
                    Reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.InapplicableOption(name).WithContext(Reader));
                }

                readResults = option.Handle(this, Resources, name, start, UseBedrock);
                if (!readResults.Successful) return readResults;

                Reader.SkipWhitespace();
                if (!Reader.CanRead()) break;
                if (Reader.Peek() == ',') {
                    Reader.Skip();
                    continue;
                }
                if (Reader.Peek() == ']') {
                    Reader.Skip();
                    return ReadResults.Success();
                }
                break;
            }
            if (Reader.CanRead() && Reader.Peek() == ']') {
                Reader.Skip();
                return ReadResults.Success();
            }
            return ReadResults.Failure(CommandError.SelectorExpectedEndOfOptions().WithContext(Reader));
        }

        private bool WillBeInverted() {
            return Reader.CanRead() && Reader.Peek() == '!';
        }

        public bool ShouldInvertValue() {
            Reader.SkipWhitespace();
            if (WillBeInverted()) {
                Reader.Skip();
                Reader.SkipWhitespace();
                return true;
            }
            return false;
        }

        public void Apply(string name, bool wasInverted) {
            if (!AppliedOptions.ContainsKey(name)) {
                AppliedOptions.Add(name, wasInverted);
            }
        }

        public void AddArgument(ParsedArgument value) {
            if (value != null) Arguments.Add(value);
        }

        private EntitySelector GetSelector() {
            return new EntitySelector(IncludesEntities, MaxResults, IsSelf, SelectorType, Arguments);
        }
    }
}

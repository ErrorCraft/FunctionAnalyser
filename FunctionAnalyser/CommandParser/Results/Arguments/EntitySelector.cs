using CommandParser.Context;
using System.Collections.Generic;

namespace CommandParser.Results.Arguments
{
    public enum SelectorType
    {
        None = 0,
        NearestPlayer = 1,
        AllPlayers = 2,
        RandomPlayer = 3,
        AllEntities = 4,
        Self = 5
    }

    public class EntitySelector
    {
        public EntitySelector(bool includesEntities, int maxResults, bool isSelf, SelectorType selectorType, List<ParsedArgument> arguments)
        {
            IncludesEntities = includesEntities;
            MaxResults = maxResults;
            IsSelf = isSelf;
            SelectorType = selectorType;
            Arguments = arguments;
        }

        public bool IncludesEntities { get; }
        public int MaxResults { get; }
        public bool IsSelf { get; }
        public SelectorType SelectorType { get; }
        public List<ParsedArgument> Arguments { get; }
    }
}

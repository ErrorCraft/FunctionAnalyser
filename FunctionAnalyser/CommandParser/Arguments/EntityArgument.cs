﻿using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class EntityArgument : IArgument<EntitySelector>
    {
        [JsonProperty("single_entity")]
        private readonly bool SingleEntity;

        [JsonProperty("players_only")]
        private readonly bool PlayersOnly;

        public EntityArgument(bool singleEntity = false, bool playersOnly = false)
        {
            SingleEntity = singleEntity;
            PlayersOnly = playersOnly;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out EntitySelector result)
        {
            int start = reader.GetCursor();
            EntitySelectorParser entitySelectorParser = new EntitySelectorParser(reader, resources);
            ReadResults readResults = entitySelectorParser.Parse(out result);
            if (!readResults.Successful) return readResults;

            if (result.MaxResults > 1 && SingleEntity)
            {
                reader.SetCursor(start);
                if (PlayersOnly)
                {
                    return new ReadResults(false, CommandError.SelectorTooManyPlayers().WithContext(reader));
                } else
                {
                    return new ReadResults(false, CommandError.SelectorTooManyEntities().WithContext(reader));
                }
            }
            if (result.IncludesEntities && PlayersOnly && !result.IsSelf)
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.SelectorPlayersOnly().WithContext(reader));
            }

            return new ReadResults(true, null);
        }
    }
}

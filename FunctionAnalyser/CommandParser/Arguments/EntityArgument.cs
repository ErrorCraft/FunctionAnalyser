using CommandParser.Parsers;
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

        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public EntityArgument(bool singleEntity = false, bool playersOnly = false, bool useBedrock = false)
        {
            SingleEntity = singleEntity;
            PlayersOnly = playersOnly;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out EntitySelector result)
        {
            int start = reader.GetCursor();
            EntitySelectorParser entitySelectorParser = new EntitySelectorParser(reader, resources, UseBedrock);
            ReadResults readResults = entitySelectorParser.Parse(out result);
            if (!readResults.Successful) return readResults;

            if (result.MaxResults > 1 && SingleEntity)
            {
                reader.SetCursor(start);
                if (PlayersOnly)
                {
                    return ReadResults.Failure(CommandError.SelectorTooManyPlayers().WithContext(reader));
                } else
                {
                    return ReadResults.Failure(CommandError.SelectorTooManyEntities().WithContext(reader));
                }
            }
            if (result.IncludesEntities && PlayersOnly && !result.IsSelf)
            {
                reader.SetCursor(start);
                return ReadResults.Failure(CommandError.SelectorPlayersOnly().WithContext(reader));
            }

            return ReadResults.Success();
        }
    }
}

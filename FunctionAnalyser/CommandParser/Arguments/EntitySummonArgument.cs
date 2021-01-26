using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class EntitySummonArgument : IArgument<Entity>
    {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Entity result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation entity);
            if (!readResults.Successful) return readResults;

            // Temporary
            if (UseBedrock)
            {
                result = new Entity(entity);
                return new ReadResults(true, null);
            }

            if (!resources.Entities.Contains(entity))
            {
                return new ReadResults(false, CommandError.UnknownEntity(entity));
            }
            result = new Entity(entity);
            return new ReadResults(true, null);
        }
    }
}

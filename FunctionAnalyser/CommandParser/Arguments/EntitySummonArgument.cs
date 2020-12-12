using CommandParser.Collections;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class EntitySummonArgument : IArgument<Entity>
    {
        public ReadResults Parse(IStringReader reader, out Entity result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation entity);
            if (!readResults.Successful) return readResults;
            if (!Entities.Contains(entity))
            {
                return new ReadResults(false, CommandError.UnknownEntity(entity));
            }
            result = new Entity(entity);
            return new ReadResults(true, null);
        }
    }
}

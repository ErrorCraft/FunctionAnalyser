using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class ScoreHolderArgument : IArgument<ScoreHolder>
    {
        [JsonProperty("multiple")]
        private readonly bool Multiple;

        public ScoreHolderArgument(bool multiple = true)
        {
            Multiple = multiple;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ScoreHolder result)
        {
            result = default;

            if (reader.CanRead() && reader.Peek() == '@')
            {
                EntitySelectorParser entitySelectorParser = new EntitySelectorParser(reader, resources);
                ReadResults readResults = entitySelectorParser.Parse(out EntitySelector entitySelector);
                if (!readResults.Successful) return readResults;
                if (!Multiple && entitySelector.MaxResults > 1)
                {
                    return new ReadResults(false, CommandError.SelectorTooManyEntities());
                }
                result = new ScoreHolder(null, entitySelector);
                return new ReadResults(true, null);
            }

            int start = reader.GetCursor();
            while (!reader.AtEndOfArgument())
            {
                reader.Skip();
            }
            string name = reader.GetString()[start..reader.GetCursor()];

            result = new ScoreHolder(name, null);
            return new ReadResults(true, null);
        }
    }
}

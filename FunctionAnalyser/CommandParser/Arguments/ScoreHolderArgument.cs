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

        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ScoreHolderArgument(bool multiple = true)
        {
            Multiple = multiple;
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ScoreHolder result)
        {
            result = default;

            if (reader.CanRead() && reader.Peek() == '@')
            {
                EntitySelectorParser entitySelectorParser = new EntitySelectorParser(reader, resources, UseBedrock);
                ReadResults readResults = entitySelectorParser.Parse(out EntitySelector entitySelector);
                if (!readResults.Successful) return readResults;
                if (!Multiple && entitySelector.MaxResults > 1)
                {
                    return new ReadResults(false, CommandError.SelectorTooManyEntities());
                }
                result = new ScoreHolder(null, entitySelector);
                return new ReadResults(true, null);
            }

            if (UseBedrock)
            {
                ReadResults readResults = reader.ReadString(out string name);
                if (!readResults.Successful) return readResults;

                result = new ScoreHolder(name, null);
                return new ReadResults(true, null);
            } else
            {
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
}

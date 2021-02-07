using CommandParser.Results;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Arguments
{
    public class SwizzleArgument : IArgument<HashSet<char>>
    {
        [JsonProperty("characters")]
        private readonly HashSet<char> Characters;

        public SwizzleArgument(HashSet<char> characters = null)
        {
            Characters = characters ?? new HashSet<char>();
        }

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out HashSet<char> result)
        {
            result = new HashSet<char>();
            int start = reader.GetCursor();

            while (!reader.AtEndOfArgument())
            {
                char c = reader.Read();
                if (!Characters.Contains(c) || result.Contains(c))
                {
                    reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.InvalidSwizzle(Characters).WithContext(reader));
                }
                result.Add(c);
            }
            return ReadResults.Success();
        }
    }
}

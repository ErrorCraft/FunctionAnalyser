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

        public ReadResults Parse(StringReader reader, out HashSet<char> result)
        {
            result = new HashSet<char>();
            int start = reader.Cursor;

            while (!reader.AtEndOfArgument())
            {
                char c = reader.Read();
                if (!Characters.Contains(c) || result.Contains(c))
                {
                    reader.Cursor = start;
                    return new ReadResults(false, CommandError.InvalidSwizzle(Characters).WithContext(reader));
                }
                result.Add(c);
            }
            return new ReadResults(true, null);
        }
    }
}

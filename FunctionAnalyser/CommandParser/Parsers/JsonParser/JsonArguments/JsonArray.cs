using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;
using System.Collections.Generic;
using CommandParser.Results;
using CommandParser.Parsers.ComponentParser;

namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonArray : IJsonArgument
    {
        public const string Name = "Array";

        private readonly List<IJsonArgument> Arguments;

        public JsonArray()
        {
            Arguments = new List<IJsonArgument>();
        }

        public void Add(IJsonArgument argument)
        {
            Arguments.Add(argument);
        }

        public string AsJson()
        {
            string s = $"{ARRAY_OPEN_CHARACTER}";
            foreach (IJsonArgument argument in Arguments)
            {
                s += $"{argument.AsJson()}{ARGUMENT_SEPARATOR}";
            }
            return s.TrimEnd(ARGUMENT_SEPARATOR) + ARRAY_CLOSE_CHARACTER;
        }

        public string GetName() => Name;

        public ReadResults ValidateComponent(IStringReader reader, int start, DispatcherResources resources)
        {
            if (Arguments.Count == 0)
            {
                reader.SetCursor(start);
                return new ReadResults(false, ComponentCommandError.EmptyComponent().WithContext(reader));
            }
            ReadResults readResults;
            for (int i = 0; i < Arguments.Count; i++)
            {
                readResults = Arguments[i].ValidateComponent(reader, start, resources);
                if (!readResults.Successful) return readResults;
            }
            return new ReadResults(true, null);
        }

        public List<IJsonArgument> GetChildren()
        {
            return Arguments;
        }
    }
}

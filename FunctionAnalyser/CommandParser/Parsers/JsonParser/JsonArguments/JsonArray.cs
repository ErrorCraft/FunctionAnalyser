using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;
using System.Collections.Generic;

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

        public int GetLength()
        {
            return Arguments.Count;
        }

        public IJsonArgument this[int index] { get { return Arguments[index]; } }

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

        public List<IJsonArgument> GetChildren()
        {
            return Arguments;
        }
    }
}

using CommandParser.Parsers.ComponentParser.ComponentArguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Components
    {
        private readonly Dictionary<string, ComponentArgument> Primary;
        private readonly Dictionary<string, ComponentArgument> Optional;

        public Components() : this(new Dictionary<string, ComponentArgument>(), new Dictionary<string, ComponentArgument>()) { }

        public Components(Dictionary<string, ComponentArgument> primary, Dictionary<string, ComponentArgument> optional)
        {
            Primary = primary;
            Optional = optional;
        }

        public Dictionary<string, ComponentArgument> GetPrimary()
        {
            return Primary;
        }

        public Dictionary<string, ComponentArgument> GetOptional()
        {
            return Optional;
        }
    }
}

using CommandParser.Parsers.ComponentParser.ComponentArguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Components
    {
        private readonly ComponentArgument Root;
        private readonly Dictionary<string, ComponentArgument> Primary;
        private readonly Dictionary<string, ComponentArgument> Optional;

        public Components() : this(null, new Dictionary<string, ComponentArgument>(), new Dictionary<string, ComponentArgument>()) { }

        public Components(ComponentArgument root, Dictionary<string, ComponentArgument> primary, Dictionary<string, ComponentArgument> optional)
        {
            Root = root;
            Primary = primary;
            Optional = optional;
        }

        public ComponentArgument GetRootComponent()
        {
            return Root;
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

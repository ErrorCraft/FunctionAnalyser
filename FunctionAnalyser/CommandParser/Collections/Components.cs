using CommandParser.Parsers.ComponentParser.ComponentArguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Components
    {
        private readonly Dictionary<string, ComponentArgument> Content;
        private readonly Dictionary<string, ComponentArgument> Children;
        private readonly Dictionary<string, ComponentArgument> Formatting;
        private readonly Dictionary<string, ComponentArgument> Interactivity;

        public Components() : this(new Dictionary<string, ComponentArgument>(), new Dictionary<string, ComponentArgument>(), new Dictionary<string, ComponentArgument>(), new Dictionary<string, ComponentArgument>()) { }

        public Components(Dictionary<string, ComponentArgument> content, Dictionary<string, ComponentArgument> children, Dictionary<string, ComponentArgument> formatting, Dictionary<string, ComponentArgument> interactivity)
        {
            Content = content;
            Children = children;
            Formatting = formatting;
            Interactivity = interactivity;
        }

        public Dictionary<string, ComponentArgument> GetContent()
        {
            return Content;
        }

        public Dictionary<string, ComponentArgument> GetChildren()
        {
            return Children;
        }

        public Dictionary<string, ComponentArgument> GetFormatting()
        {
            return Formatting;
        }

        public Dictionary<string, ComponentArgument> GetInteractivity()
        {
            return Interactivity;
        }
    }
}

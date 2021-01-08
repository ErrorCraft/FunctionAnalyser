using CommandParser.Parsers.ComponentParser.ComponentArguments;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Components
    {
        private static Dictionary<string, ComponentArgument> ContentObsolete = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> ChildrenObsolete = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> FormattingObsolete = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> InteractivityObsolete = new Dictionary<string, ComponentArgument>();
        private readonly Dictionary<string, ComponentArgument> Content;
        private readonly Dictionary<string, ComponentArgument> Children;
        private readonly Dictionary<string, ComponentArgument> Formatting;
        private readonly Dictionary<string, ComponentArgument> Interactivity;

        public Components(Dictionary<string, ComponentArgument> content, Dictionary<string, ComponentArgument> children, Dictionary<string, ComponentArgument> formatting, Dictionary<string, ComponentArgument> interactivity)
        {
            Content = content;
            Children = children;
            Formatting = formatting;
            Interactivity = interactivity;
        }

        public static void Set(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.TryGetValue("content", out JToken contentJson)) ContentObsolete = contentJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("children", out JToken childrenJson)) ChildrenObsolete = childrenJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("formatting", out JToken formattingJson)) FormattingObsolete = formattingJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("interactivity", out JToken interactivityJson)) InteractivityObsolete = interactivityJson.ToObject<Dictionary<string, ComponentArgument>>();
        }

        public static bool Contains()
        {
            return false;
        }

        public static Dictionary<string, ComponentArgument> GetContent()
        {
            return ContentObsolete;
        }

        public static Dictionary<string, ComponentArgument> GetChildren()
        {
            return ChildrenObsolete;
        }

        public static Dictionary<string, ComponentArgument> GetFormatting()
        {
            return FormattingObsolete;
        }

        public static Dictionary<string, ComponentArgument> GetInteractivity()
        {
            return InteractivityObsolete;
        }
    }
}

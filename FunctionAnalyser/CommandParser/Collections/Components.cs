using CommandParser.Parsers.ComponentParser.ComponentArguments;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class Components
    {
        private static Dictionary<string, ComponentArgument> Content = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> Children = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> Formatting = new Dictionary<string, ComponentArgument>();
        private static Dictionary<string, ComponentArgument> Interactivity = new Dictionary<string, ComponentArgument>();

        public static void Set(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.TryGetValue("content", out JToken contentJson)) Content = contentJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("children", out JToken childrenJson)) Children = childrenJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("formatting", out JToken formattingJson)) Formatting = formattingJson.ToObject<Dictionary<string, ComponentArgument>>();
            if (jObject.TryGetValue("interactivity", out JToken interactivityJson)) Interactivity = interactivityJson.ToObject<Dictionary<string, ComponentArgument>>();
        }

        public static bool Contains()
        {
            return false;
        }

        public static Dictionary<string, ComponentArgument> GetContent()
        {
            return Content;
        }

        public static Dictionary<string, ComponentArgument> GetChildren()
        {
            return Children;
        }

        public static Dictionary<string, ComponentArgument> GetFormatting()
        {
            return Formatting;
        }

        public static Dictionary<string, ComponentArgument> GetInteractivity()
        {
            return Interactivity;
        }
    }
}

using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Blocks
    {
        private static Dictionary<string, BlockState> Options = new Dictionary<string, BlockState>();
        private readonly Dictionary<string, BlockState> Values;

        public Blocks(Dictionary<string, BlockState> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, BlockState>>(json);
        }

        public static bool ContainsBlock(ResourceLocation item)
        {
            return item.IsDefaultNamespace() && Options.ContainsKey(item.Path);
        }

        public static bool ContainsProperty(ResourceLocation item, string property)
        {
            return Options[item.Path].ContainsProperty(property);
        }

        public static bool PropertyContainsValue(ResourceLocation item, string property, string value)
        {
            return Options[item.Path].PropertyContainsValue(property, value);
        }
    }
}

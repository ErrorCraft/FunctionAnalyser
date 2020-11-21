using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class MobEffects
    {
        private static HashSet<string> Options = new HashSet<string>();

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(ResourceLocation effect)
        {
            return effect.IsDefaultNamespace() && Options.Contains(effect.Path);
        }
    }
}

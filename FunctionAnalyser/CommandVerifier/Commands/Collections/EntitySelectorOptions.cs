using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class EntitySelectorOptions
    {
        public static Dictionary<string, EntitySelectorOption> Options { get; private set; }
        
        static EntitySelectorOptions()
        {
            Options = new Dictionary<string, EntitySelectorOption>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, EntitySelectorOption>>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }

        public static bool TryGet(string input, StringReader reader, bool may_throw, out EntitySelectorOption option)
        {
            option = null;
            if (Options.ContainsKey(input))
            {
                option = Options[input];
                return true;
            }

            if (may_throw) CommandError.UnknownSelectorOption(input).AddWithContext(reader);
            return false;
        }
    }
}

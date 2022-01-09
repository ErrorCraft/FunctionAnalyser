using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Pack;

public class PackDefinition {
    private readonly string Root;

    public PackDefinition(string folder) {
        Root = folder;
    }

    public class Serialiser : IJsonSerialiser<PackDefinition> {
        public void ToJson(JObject json, PackDefinition value, JsonSerializer serialiser) {
            json.Add("root", value.Root);
        }

        public PackDefinition FromJson(JObject json, JsonSerializer serialiser) {
            string folder = json.GetString("root");
            return new PackDefinition(folder);
        }
    }
}

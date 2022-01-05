using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Pack;

public class PackRoot {
    private readonly string Folder;
    private readonly string Metadata;

    public PackRoot(string folder, string metadata) {
        Folder = folder;
        Metadata = metadata;
    }

    public class Serialiser : IJsonSerialiser<PackRoot> {
        public void ToJson(JObject json, PackRoot value, JsonSerializer serialiser) {
            json.Add("folder", value.Folder);
            json.Add("metadata", value.Metadata);
        }

        public PackRoot FromJson(JObject json, JsonSerializer serialiser) {
            string folder = json.GetString("folder");
            string metadata = json.GetString("metadata");
            return new PackRoot(folder, metadata);
        }
    }
}

using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Pack;

public class PackVersion {
    private readonly PackRoot Root;

    public PackVersion(PackRoot root) {
        Root = root;
    }

    public class Serialiser : IJsonSerialiser<PackVersion> {
        public void ToJson(JObject json, PackVersion value, JsonSerializer serialiser) {
            json.Add("root", serialiser.Serialise(value.Root));
        }

        public PackVersion FromJson(JObject json, JsonSerializer serialiser) {
            PackRoot root = json.Deserialise<PackRoot>("root", serialiser);
            return new PackVersion(root);
        }
    }
}

using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Pack;

public class PackMetadata {
    private readonly string FileName;
    private readonly JsonValidator Validator;

    public PackMetadata(string fileName, JsonValidator validator) {
        FileName = fileName;
        Validator = validator;
    }

    public class Serialiser : IJsonSerialiser<PackMetadata> {
        public void ToJson(JObject json, PackMetadata value, JsonSerializer serialiser) {
            json.Add("file", value.FileName);
            json.Add("validator", serialiser.Serialise(value.Validator));
        }

        public PackMetadata FromJson(JObject json, JsonSerializer serialiser) {
            string fileName = json.GetString("file");
            JsonValidator validator = json.Deserialise<JsonValidator>("validator", serialiser);
            return new PackMetadata(fileName, validator);
        }
    }
}

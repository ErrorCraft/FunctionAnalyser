using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ErrorCraft.Minecraft.Pack;

public class PackMetadata {
    private readonly string FileName;
    private readonly JsonValidator Validator;

    public PackMetadata(string fileName, JsonValidator validator) {
        FileName = fileName;
        Validator = validator;
    }

    public Result<IJsonValidated> Analyse(string path) {
        string metadataFile = Path.Combine(path, FileName);
        if (!File.Exists(metadataFile)) {
            return Result<IJsonValidated>.Failure(new Message($"Metadata file ({FileName}) does not exist"));
        }
        Result<IJsonElement> jsonResult = Json.JsonReader.ReadFromFile(metadataFile);
        if (!jsonResult.Successful) {
            return Result<IJsonValidated>.Failure(jsonResult.Message);
        }
        return Validator.Validate(jsonResult.Value, "root");
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

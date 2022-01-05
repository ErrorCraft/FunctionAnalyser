using ErrorCraft.Minecraft.Util.Json;

namespace ErrorCraft.Minecraft.Json.Validating;

public class JsonValidatorType : JsonSerialiserType<JsonValidator> {
    public JsonValidatorType(IJsonSerialiser<JsonValidator> serialiser) : base(serialiser) { }
}

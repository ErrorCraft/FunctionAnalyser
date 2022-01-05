using ErrorCraft.Minecraft.Util.Json;

namespace ErrorCraft.Minecraft.Json.Validating;

public class JsonValidatorType : JsonSerialiserType<JsonValidator> {
    public JsonValidatorType(JsonSerialiser<JsonValidator> serialiser) : base(serialiser) { }
}

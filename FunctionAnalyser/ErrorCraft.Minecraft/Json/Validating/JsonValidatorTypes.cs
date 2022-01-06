using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using ErrorCraft.Minecraft.Util.Registries;

namespace ErrorCraft.Minecraft.Json.Validating;

public static class JsonValidatorTypes {
    public static readonly Registry<JsonValidatorType> JSON_VALIDATOR_TYPE = new Registry<JsonValidatorType>();

    public static readonly JsonValidatorType BOOLEAN = Register("boolean", new BooleanJsonValidator.Serialiser());
    public static readonly JsonValidatorType NUMBER = Register("number", new NumberJsonValidator.Serialiser());
    public static readonly JsonValidatorType STRING = Register("string", new StringJsonValidator.Serialiser());
    public static readonly JsonValidatorType OBJECT = Register("object", new ObjectJsonValidator.Serialiser());
    public static readonly JsonValidatorType ARRAY = Register("array", new ArrayJsonValidator.Serialiser());

    public static IJsonSerialiser<JsonValidator> CreateJsonSerialiser() {
        return new RegistryJsonSerialiser<JsonValidator, JsonValidatorType>(JSON_VALIDATOR_TYPE, "type");
    }

    private static JsonValidatorType Register(string id, JsonValidator.Serialiser jsonValidator) {
        return JSON_VALIDATOR_TYPE.Register(new ResourceLocation("json", id), new JsonValidatorType(jsonValidator));
    }
}

using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Pack.Resources;

public class JsonValidatorTypeCollection {
    private readonly string Key;
    private readonly ObjectJsonValidator Template;
    private readonly ResourceLocationCollection<ObjectJsonValidator> Types;

    public JsonValidatorTypeCollection(string key, ObjectJsonValidator template, ResourceLocationCollection<ObjectJsonValidator> types) {
        Key = key;
        Template = template;
        Types = types;
    }

    public Result Validate(JsonObject json) {
        Result<ExactResourceLocation> resourceLocationResult = json.GetResourceLocation(Key);
        if (!resourceLocationResult.Successful) {
            return Result.Failure(resourceLocationResult.Message);
        }
        return Validate(json, resourceLocationResult.Value);
    }

    private Result Validate(JsonObject json, ExactResourceLocation resourceLocation) {
        if (!Types.TryGetValue(resourceLocation, out ObjectJsonValidator? validator)) {
            return Result.Failure(new Message($"Invalid {Key} '{resourceLocation}'"));
        }

        Result itemValidateResult = validator.Validate(json, "item");
        if (!itemValidateResult.Successful) {
            return itemValidateResult;
        }

        Result templateValidateResult = Template.Validate(json, "item");
        if (!templateValidateResult.Successful) {
            return templateValidateResult;
        }

        return Result.Success();
    }

    public class Serialiser : IJsonSerialiser<JsonValidatorTypeCollection> {
        public void ToJson(JObject json, JsonValidatorTypeCollection value, JsonSerializer serialiser) {
            json.Add("key", value.Key);
            json.Add("template", serialiser.Serialise(value.Template));
            json.Add("types", serialiser.Serialise(value.Types));
        }

        public JsonValidatorTypeCollection FromJson(JObject json, JsonSerializer serialiser) {
            string key = json.GetString("key");
            ObjectJsonValidator template = json.Deserialise<ObjectJsonValidator>("template", serialiser);
            ResourceLocationCollection<ObjectJsonValidator> types = json.Deserialise<ResourceLocationCollection<ObjectJsonValidator>>("types", serialiser);
            return new JsonValidatorTypeCollection(key, template, types);
        }
    }
}

using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Resources.Json;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Extensions;
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

    public Result<ValidatedJsonObject> Validate(IJsonElement json) {
        Result<JsonObject> objectResult = json.AsObject("item");
        if (!objectResult.Successful) {
            return Result<ValidatedJsonObject>.Failure(objectResult);
        }
        return Validate(objectResult.Value);
    }

    public Result<ValidatedJsonObject> Validate(JsonObject json) {
        Result<ExactResourceLocation> resourceLocationResult = json.GetResourceLocation(Key);
        if (!resourceLocationResult.Successful) {
            return Result<ValidatedJsonObject>.Failure(resourceLocationResult.Message);
        }
        return Validate(json, resourceLocationResult.Value);
    }

    public static OptionalLoadableJsonResource<JsonValidatorTypeCollection> CreateLoader(string fileName) {
        return new OptionalLoadableJsonResource<JsonValidatorTypeCollection>(fileName,
            JsonSerialiserConverterExtensions.Create(new Serialiser()),
            JsonSerialiserConverterExtensions.Create(new ResourceLocationCollection<ObjectJsonValidator>.Serialiser()),
            JsonSerialiserConverterExtensions.Create(new ObjectJsonValidator.Serialiser()),
            JsonSerialiserConverterExtensions.Create(JsonValidatorTypes.CreateJsonSerialiser()));
    }

    private Result<ValidatedJsonObject> Validate(JsonObject json, ExactResourceLocation resourceLocation) {
        if (!Types.TryGetValue(resourceLocation, out ObjectJsonValidator? validator)) {
            return Result<ValidatedJsonObject>.Failure(new Message($"Invalid {Key} '{resourceLocation}'"));
        }

        Result<ValidatedJsonObject> itemValidateResult = validator.Validate(json, "item");
        if (!itemValidateResult.Successful) {
            return itemValidateResult;
        }

        Result<ValidatedJsonObject> templateValidateResult = Template.Validate(json, "item");
        if (!templateValidateResult.Successful) {
            return templateValidateResult;
        }

        itemValidateResult.Value.Merge(templateValidateResult.Value);
        return Result<ValidatedJsonObject>.Success(itemValidateResult.Value);
    }

    public class Serialiser : IJsonSerialiser<JsonValidatorTypeCollection> {
        public JsonValidatorTypeCollection FromJson(JObject json, JsonSerializer serialiser) {
            string key = json.GetString("key");
            ObjectJsonValidator template = json.Deserialise<ObjectJsonValidator>("template", serialiser);
            ResourceLocationCollection<ObjectJsonValidator> types = json.Deserialise<ResourceLocationCollection<ObjectJsonValidator>>("types", serialiser);
            return new JsonValidatorTypeCollection(key, template, types);
        }
    }
}

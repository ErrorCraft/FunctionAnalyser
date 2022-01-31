using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class ArrayJsonValidator : JsonValidator {
    private readonly JsonValidator Items;

    public ArrayJsonValidator(bool optional, JsonValidator content) : base(optional) {
        Items = content;
    }

    public override Result<IJsonValidated> Validate(IJsonElement json, string name) {
        Result<JsonArray> result = json.AsArray(name);
        if (!result.Successful) {
            return Result<IJsonValidated>.Failure(result);
        }
        return Validate(result.Value);
    }

    private Result<IJsonValidated> Validate(JsonArray json) {
        ValidatedJsonArray resultingArray = new ValidatedJsonArray();
        foreach (IJsonElement item in json) {
            Result<IJsonValidated> result = Items.Validate(item, "item");
            if (!result.Successful) {
                return result;
            }
            resultingArray.Add(result.Value);
        }
        return Result<IJsonValidated>.Success(resultingArray);
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override ArrayJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            JsonValidator content = json.Deserialise<JsonValidator>("items", serialiser);
            return new ArrayJsonValidator(optional, content);
        }
    }
}

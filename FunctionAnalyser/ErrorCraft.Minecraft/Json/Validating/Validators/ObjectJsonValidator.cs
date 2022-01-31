using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class ObjectJsonValidator : JsonValidator {
    private readonly Dictionary<string, JsonValidator> Children;

    public ObjectJsonValidator(bool optional, Dictionary<string, JsonValidator> children) : base(optional) {
        Children = children;
    }

    public override Result<IJsonValidated> Validate(IJsonElement json, string name) {
        Result<JsonObject> result = json.AsObject(name);
        if (!result.Successful) {
            return Result<IJsonValidated>.Failure(result);
        }
        return Validate(result.Value, name);
    }

    private Result<IJsonValidated> Validate(JsonObject json, string name) {
        if (Children == null) {
            return Result<IJsonValidated>.Success(new ValidatedJsonObject());
        }
        ValidatedJsonObject resultingObject = new ValidatedJsonObject();
        foreach (KeyValuePair<string, JsonValidator> pair in Children) {
            Result<IJsonValidated> result = Validate(json, name, pair.Key, pair.Value);
            if (!result.Successful) {
                return result;
            }
            resultingObject.Add(pair.Key, result.Value);
        }
        return Result<IJsonValidated>.Success(resultingObject);
    }

    private static Result<IJsonValidated> Validate(JsonObject json, string name, string childName, JsonValidator validator) {
        if (!json.TryGetValue(childName, out IJsonElement? child)) {
            return Result<IJsonValidated>.Failure(new Message($"Expected {name} to have a child named {childName}"));
        }
        return validator.Validate(child, name);
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override ObjectJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            Dictionary<string, JsonValidator> children = json.Deserialise<Dictionary<string, JsonValidator>>("children", serialiser);
            return new ObjectJsonValidator(optional, children);
        }
    }
}

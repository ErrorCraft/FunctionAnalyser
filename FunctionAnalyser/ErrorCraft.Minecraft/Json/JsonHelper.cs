using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.ResourceLocations;

namespace ErrorCraft.Minecraft.Json;

internal static class JsonHelper {
    public static Result<ExactResourceLocation> GetResourceLocation(this JsonObject json, string key) {
        Result<string> result = json.GetString(key);
        if (!result.Successful) {
            return Result<ExactResourceLocation>.Failure(result);
        }
        if (!ExactResourceLocation.TryParse(result.Value, out ExactResourceLocation? resourceLocation)) {
            return Result<ExactResourceLocation>.Failure(new Message($"Invalid resource location '{result.Value}'"));
        }
        return Result<ExactResourceLocation>.Success(resourceLocation);
    }

    public static Result<bool> AsBoolean(this IJsonElement json, string name) {
        if (json is JsonBoolean jsonBoolean) {
            return Result<bool>.Success((bool)jsonBoolean);
        }
        return Result<bool>.Failure(new Message($"Expected {name} to be a boolean"));
    }

    public static Result<double> AsDouble(this IJsonElement json, string name) {
        if (json is JsonNumber jsonNumber) {
            return Result<double>.Success((double)jsonNumber);
        }
        return Result<double>.Failure(new Message($"Expected {name} to be a number"));
    }

    public static Result<string> GetString(this JsonObject json, string key) {
        if (!json.TryGetValue(key, out IJsonElement? jsonElement)) {
            return Result<string>.Failure(new Message($"Missing {key}, expected to find a string"));
        }
        return jsonElement.AsString(key);
    }

    public static Result<string> AsString(this IJsonElement json, string name) {
        if (json is JsonString jsonString) {
            return Result<string>.Success((string)jsonString);
        }
        return Result<string>.Failure(new Message($"Expected {name} to be a string"));
    }

    public static Result<JsonObject> AsObject(this IJsonElement json, string name) {
        if (json is JsonObject jsonObject) {
            return Result<JsonObject>.Success(jsonObject);
        }
        return Result<JsonObject>.Failure(new Message($"Expected {name} to be an object"));
    }

    public static Result<JsonArray> AsArray(this IJsonElement json, string name) {
        if (json is JsonArray jsonArray) {
            return Result<JsonArray>.Success(jsonArray);
        }
        return Result<JsonArray>.Failure(new Message($"Expected {name} to be an array"));
    }
}

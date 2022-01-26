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
}

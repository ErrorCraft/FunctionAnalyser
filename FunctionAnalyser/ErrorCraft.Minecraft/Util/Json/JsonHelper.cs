using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Util.Json;

internal static class JsonHelper {
    public static string GetString(this JObject json, string key) {
        if (!json.TryGetValue(key, out JToken? token)) {
            throw new JsonException($"Missing {key}, expected to find a string");
        }
        return AsString(token, key);
    }

    public static string AsString(this JToken json, string name) {
        if (json.Type != JTokenType.String) {
            throw new JsonException($"Expected {name} to be a string, was {json.Type}");
        }
        return json.Value<string>()!;
    }

    public static bool GetBoolean(this JObject json, string key) {
        if (!json.TryGetValue(key, out JToken? token)) {
            throw new JsonException($"Missing {key}, expected to find a boolean");
        }
        return AsBoolean(token, key);
    }

    public static bool GetBoolean(this JObject json, string key, bool defaultValue) {
        if (!json.TryGetValue(key, out JToken? token)) {
            return defaultValue;
        }
        return AsBoolean(token, key);
    }

    public static bool AsBoolean(this JToken json, string name) {
        if (json.Type != JTokenType.Boolean) {
            throw new JsonException($"Expected {name} to be a boolean, was {json.Type}");
        }
        return json.Value<bool>()!;
    }

    public static T Deserialise<T>(this JObject json, string key, JsonSerializer serialiser) {
        if (!json.TryGetValue(key, out JToken? token)) {
            throw new JsonException($"Missing {key}");
        }
        return token.Deserialise<T>(key, serialiser);
    }

    public static T Deserialise<T>(this JToken json, string name, JsonSerializer serialiser) {
        if (json == null) {
            throw new JsonException($"Missing {name}");
        }
        using JsonReader reader = json.CreateReader();
        return serialiser.Deserialize<T>(reader)!;
    }

    public static JObject Serialise<T>(this JsonSerializer serialiser, T value) {
        JObject json = new JObject();
        using JsonWriter writer = json.CreateWriter();
        serialiser.Serialize(writer, value);
        return json;
    }
}

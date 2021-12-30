using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ErrorCraft.Minecraft.Util.Json;

public abstract class JsonSerialiser<T> : JsonConverter<T> {
    public sealed override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer) {
        if (value == null) {
            return;
        }
        JObject json = new JObject();
        ToJson(json, value, serializer);
        json.WriteTo(writer);
    }

    public sealed override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer) {
        JObject json = JObject.Load(reader);
        return FromJson(json, serializer);
    }

    public abstract void ToJson(JObject json, T value, JsonSerializer serialiser);
    public abstract T FromJson(JObject json, JsonSerializer serialiser);
}

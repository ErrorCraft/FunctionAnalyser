using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ErrorCraft.Minecraft.Util.Json;

public class JsonSerialiserConverter<T> : JsonConverter<T> {
    private readonly IJsonSerialiser<T> Serialiser;

    public JsonSerialiserConverter(IJsonSerialiser<T> serialiser) {
        Serialiser = serialiser;
    }

    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer) {
        if (value == null) {
            return;
        }
        JObject json = new JObject();
        Serialiser.ToJson(json, value, serializer);
        json.WriteTo(writer);
    }

    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer) {
        JObject json = JObject.Load(reader);
        return Serialiser.FromJson(json, serializer);
    }
}

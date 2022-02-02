using ErrorCraft.Minecraft.Util.Json;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ErrorCraft.Minecraft.Util.Registries;

public class RegistryJsonSerialiser<U, T> : IJsonSerialiser<U> where T : JsonSerialiserType<U> {
    private readonly Registry<T> Registry;
    private readonly string RootField;

    public RegistryJsonSerialiser(Registry<T> registry, string rootField) {
        Registry = registry;
        RootField = rootField;
    }

    public U FromJson(JObject json, JsonSerializer serialiser) {
        string s = json.GetString(RootField);
        if (!ExactResourceLocation.TryParse(s, out ExactResourceLocation? resourceLocation)) {
            throw new JsonException($"Invalid resource location {s}");
        }
        T item = Registry[resourceLocation];
        return item.Serialiser.FromJson(json, serialiser);
    }
}

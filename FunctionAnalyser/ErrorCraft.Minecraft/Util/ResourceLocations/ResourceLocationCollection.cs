using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Util.ResourceLocations;

public class ResourceLocationCollection<T> : IEnumerable<KeyValuePair<ExactResourceLocation, T>> {
    private readonly Dictionary<ExactResourceLocation, T> Items;

    public ResourceLocationCollection() : this(new Dictionary<ExactResourceLocation, T>()) { }

    public ResourceLocationCollection(Dictionary<ExactResourceLocation, T> items) {
        Items = items;
    }

    public T this[ExactResourceLocation resourceLocation] {
        get { return Items[resourceLocation]; }
    }

    public IEnumerator<KeyValuePair<ExactResourceLocation, T>> GetEnumerator() {
        foreach (KeyValuePair<ExactResourceLocation, T> pair in Items) {
            yield return pair;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public bool TryGetValue(ExactResourceLocation resourceLocation, [MaybeNullWhen(false)] out T value) {
        return Items.TryGetValue(resourceLocation, out value);
    }

    public class Serialiser : IJsonSerialiser<ResourceLocationCollection<T>> {
        public ResourceLocationCollection<T> FromJson(JObject json, JsonSerializer serialiser) {
            Dictionary<ExactResourceLocation, T> items = new Dictionary<ExactResourceLocation, T>();
            foreach (KeyValuePair<string, JToken?> pair in json) {
                if (!ExactResourceLocation.TryParse(pair.Key, out ExactResourceLocation? resourceLocation)) {
                    throw new JsonException($"Invalid resource location {pair.Key}");
                }
                if (pair.Value == null) {
                    throw new JsonException($"Item '{pair.Key}' is null");
                }
                T item = pair.Value.Deserialise<T>(pair.Key, serialiser);
                items.Add(resourceLocation, item);
            }
            return new ResourceLocationCollection<T>(items);
        }
    }
}

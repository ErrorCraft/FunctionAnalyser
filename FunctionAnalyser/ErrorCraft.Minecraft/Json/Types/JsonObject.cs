using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonObject : IJsonElement {
    internal const char OBJECT_OPEN_CHARACTER = '{';
    internal const char OBJECT_CLOSE_CHARACTER = '}';
    internal const char NAME_SEPARATOR = ':';
    internal const char VALUE_SEPARATOR = ',';

    private readonly Dictionary<string, IJsonElement> Items;

    public JsonObject() : this(new Dictionary<string, IJsonElement>()) {}

    public IJsonElement this[string key] {
        get { return Items[key]; }
        set { Items[key] = value; }
    }

    public JsonObject(Dictionary<string, IJsonElement> items) {
        Items = items;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.OBJECT;
    }
}

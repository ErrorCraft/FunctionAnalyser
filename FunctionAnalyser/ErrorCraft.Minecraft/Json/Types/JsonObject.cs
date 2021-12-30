using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonObject : IJsonElement {
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

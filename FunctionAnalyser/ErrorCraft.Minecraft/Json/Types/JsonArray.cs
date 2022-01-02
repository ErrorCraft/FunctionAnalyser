using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonArray : IJsonElement {
    internal const char ARRAY_OPEN_CHARACTER = '[';
    internal const char ARRAY_CLOSE_CHARACTER = ']';

    private readonly List<IJsonElement> Items;

    public JsonArray() {
        Items = new List<IJsonElement>();
    }

    public IJsonElement this[int index] {
        get { return Items[index]; }
        set { Items[index] = value; }
    }

    public JsonArray(IEnumerable<IJsonElement> items) {
        Items = new List<IJsonElement>(items);
    }

    public JsonElementType GetElementType() {
        return JsonElementType.ARRAY;
    }
}

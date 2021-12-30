using System;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonString : IJsonElement {
    private readonly string Value;

    public JsonString(string value) {
        Value = value;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.STRING;
    }

    public static explicit operator string(JsonString? value) {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }
}

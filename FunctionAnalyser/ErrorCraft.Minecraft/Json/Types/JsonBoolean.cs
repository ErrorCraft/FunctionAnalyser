using System;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonBoolean : IJsonElement {
    private readonly bool Value;

    public JsonBoolean(bool value) {
        Value = value;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.BOOLEAN;
    }

    public static explicit operator bool(JsonBoolean? value) {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }
}

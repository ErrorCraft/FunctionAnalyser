using System;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonNumber : IJsonElement {
    private readonly double Value;

    public JsonNumber(double value) {
        Value = value;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.NUMBER;
    }

    public static explicit operator double(JsonNumber? number) {
        ArgumentNullException.ThrowIfNull(number);
        return number.Value;
    }
}

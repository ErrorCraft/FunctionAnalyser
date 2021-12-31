using ErrorCraft.Minecraft.Util;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonBoolean : IJsonElement {
    private const string TRUE_STRING = "true";
    private const string FALSE_STRING = "false";

    private readonly bool Value;

    public JsonBoolean(bool value) {
        Value = value;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.BOOLEAN;
    }

    internal static bool TryParse(string text, [NotNullWhen(true)] out JsonBoolean? result) {
        result = text switch {
            TRUE_STRING => new JsonBoolean(true),
            FALSE_STRING => new JsonBoolean(false),
            _ => null
        };
        return result != null;
    }

    public static explicit operator bool(JsonBoolean? value) {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }
}

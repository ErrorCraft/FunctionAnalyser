using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonNumber : IJsonElement {
    private readonly double Value;

    public JsonNumber(double value) {
        Value = value;
    }

    public JsonElementType GetElementType() {
        return JsonElementType.NUMBER;
    }

    internal static bool TryParse(string input, [NotNullWhen(true)] out JsonNumber? result) {
        if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)) {
            result = new JsonNumber(value);
            return true;
        }
        result = null;
        return false;
    }

    public static explicit operator double(JsonNumber? number) {
        ArgumentNullException.ThrowIfNull(number);
        return number.Value;
    }
}

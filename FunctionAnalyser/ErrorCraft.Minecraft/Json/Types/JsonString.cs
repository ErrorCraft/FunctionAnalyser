using System;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonString : IJsonElement {
    internal const char QUOTE_CHARACTER = '"';
    internal const char ESCAPE_CHARACTER = '\\';
    internal const char UNICODE_ESCAPE_CHARACTER = 'u';
    internal static readonly IReadOnlyDictionary<char, char> ESCAPE_CHARACTERS = new Dictionary<char, char>() { { '"', '"' }, { '\\', '\\' }, { '/', '/' }, { 'b', '\b' }, { 'f', '\f' }, { 'n', '\n' }, { 'r', '\r' }, { 't', '\t' } };

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

using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Json.Types;

public class JsonNull : IJsonElement {
    private const string NULL_STRING = "null";

    public static readonly JsonNull INSTANCE = new JsonNull();

    private JsonNull() {}

    public JsonElementType GetElementType() {
        return JsonElementType.NULL;
    }

    internal static bool TryParse(string input, [NotNullWhen(true)] out JsonNull? result) {
        if (input == NULL_STRING) {
            result = INSTANCE;
            return true;
        }
        result = null;
        return false;
    }
}

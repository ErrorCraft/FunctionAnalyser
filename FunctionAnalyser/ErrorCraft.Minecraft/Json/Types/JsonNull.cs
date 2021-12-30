namespace ErrorCraft.Minecraft.Json.Types;

public class JsonNull : IJsonElement {
    public static readonly JsonNull INSTANCE = new JsonNull();

    private JsonNull() {}

    public JsonElementType GetElementType() {
        return JsonElementType.NULL;
    }
}

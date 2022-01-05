namespace ErrorCraft.Minecraft.Util.Json;

public abstract class JsonSerialiserType<T> {
    public JsonSerialiser<T> Serialiser { get; }

    public JsonSerialiserType(JsonSerialiser<T> serialiser) {
        Serialiser = serialiser;
    }
}

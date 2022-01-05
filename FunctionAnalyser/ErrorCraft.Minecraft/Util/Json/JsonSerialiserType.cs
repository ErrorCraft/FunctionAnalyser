namespace ErrorCraft.Minecraft.Util.Json;

public abstract class JsonSerialiserType<T> {
    public IJsonSerialiser<T> Serialiser { get; }

    public JsonSerialiserType(IJsonSerialiser<T> serialiser) {
        Serialiser = serialiser;
    }
}

using ErrorCraft.Minecraft.Util.Json;

namespace ErrorCraft.Minecraft.Util.Extensions;

internal static class JsonSerialiserConverterExtensions {
    public static JsonSerialiserConverter<T> Create<T>(IJsonSerialiser<T> serialiser) {
        return new JsonSerialiserConverter<T>(serialiser);
    }
}

using Newtonsoft.Json;

namespace ErrorCraft.Minecraft.Resources.Json;

public class OptionalLoadableJsonResource<T> : LoadableJsonResource<T?> {
    public OptionalLoadableJsonResource(string fileName, params JsonConverter[] converters) : base(fileName, converters) { }

    protected override T? Fail() {
        return default;
    }
}

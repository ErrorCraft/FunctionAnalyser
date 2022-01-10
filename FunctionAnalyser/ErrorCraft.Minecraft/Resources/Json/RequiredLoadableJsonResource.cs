using Newtonsoft.Json;

namespace ErrorCraft.Minecraft.Resources.Json;

public class RequiredLoadableJsonResource<T> : LoadableJsonResource<T> {
    public RequiredLoadableJsonResource(string fileName, params JsonConverter[] converters) : base(fileName, converters) { }
}

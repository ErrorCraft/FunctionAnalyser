using Newtonsoft.Json;

namespace ErrorCraft.Minecraft.Resources.Json;

public abstract class LoadableJsonResource<T> : LoadableResource<T> {
    private readonly JsonConverter[] Converters;

    public LoadableJsonResource(string fileName, params JsonConverter[] converters) : base(fileName) {
        Converters = converters;
    }

    protected override T Apply(string fileContents) {
        T? result = JsonConvert.DeserializeObject<T>(fileContents, Converters);
        if (result == null) {
            return Fail();
        }
        return result;
    }
}

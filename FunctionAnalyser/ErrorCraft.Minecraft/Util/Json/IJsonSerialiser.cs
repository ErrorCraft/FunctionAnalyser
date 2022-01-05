using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Util.Json;

public interface IJsonSerialiser<T> {
    public abstract void ToJson(JObject json, T value, JsonSerializer serialiser);
    public abstract T FromJson(JObject json, JsonSerializer serialiser);
}

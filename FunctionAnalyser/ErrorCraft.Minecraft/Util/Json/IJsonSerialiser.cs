using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Util.Json;

public interface IJsonSerialiser<out T> {
    public abstract T FromJson(JObject json, JsonSerializer serialiser);
}

using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating;

public abstract class JsonValidator {
    protected readonly bool Optional;

    protected JsonValidator(bool optional) {
        Optional = optional;
    }

    public abstract Result Validate(IJsonElement json, string name);

    public abstract class Serialiser : JsonSerialiser<JsonValidator> {
        public override void ToJson(JObject json, JsonValidator value, JsonSerializer serialiser) {
            json.Add("optional", value.Optional);
        }

        public sealed override JsonValidator FromJson(JObject json, JsonSerializer serialiser) {
            bool optional = json.GetBoolean("optional", false);
            return FromJson(json, serialiser, optional);
        }

        public abstract JsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional);
    }
}

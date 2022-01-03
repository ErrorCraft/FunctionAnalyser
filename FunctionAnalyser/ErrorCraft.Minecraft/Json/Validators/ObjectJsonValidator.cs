using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validators;

public class ObjectJsonValidator : JsonValidator {
    public ObjectJsonValidator(bool optional) : base(optional) { }

    public override Result Validate(IJsonElement json, string name) {
        if (json.GetElementType() == JsonElementType.OBJECT) {
            return Result.Success();
        }
        return Result.Failure(new Message($"Expected {name} to be an object"));
    }

    public class Serialiser : Serialiser<ObjectJsonValidator> {
        public override ObjectJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new ObjectJsonValidator(optional);
        }
    }
}

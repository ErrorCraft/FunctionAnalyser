using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validators;

public class ArrayJsonValidator : JsonValidator {
    public ArrayJsonValidator(bool optional) : base(optional) { }

    public override Result Validate(IJsonElement json, string name) {
        if (json.GetElementType() == JsonElementType.ARRAY) {
            return Result.Success();
        }
        return Result.Failure(new Message($"Expected {name} to be an array"));
    }

    public class Serialiser : Serialiser<ArrayJsonValidator> {
        public override ArrayJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new ArrayJsonValidator(optional);
        }
    }
}

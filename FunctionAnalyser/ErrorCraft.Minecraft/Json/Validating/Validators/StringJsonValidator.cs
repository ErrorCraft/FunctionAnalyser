using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class StringJsonValidator : JsonValidator {
    public StringJsonValidator(bool optional) : base(optional) { }

    public override Result Validate(IJsonElement json, string name) {
        if (json.GetElementType() == JsonElementType.STRING) {
            return Result.Success();
        }
        return Result.Failure(new Message($"Expected {name} to be a string"));
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override StringJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new StringJsonValidator(optional);
        }
    }
}

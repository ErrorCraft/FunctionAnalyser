using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class NumberJsonValidator : JsonValidator {
    public NumberJsonValidator(bool optional) : base(optional) { }

    public override Result Validate(IJsonElement json, string name) {
        if (json.GetElementType() == JsonElementType.NUMBER) {
            return Result.Success();
        }
        return Result.Failure(new Message($"Expected {name} to be a number"));
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override NumberJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new NumberJsonValidator(optional);
        }
    }
}

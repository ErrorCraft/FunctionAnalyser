using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class BooleanJsonValidator : JsonValidator {
    public BooleanJsonValidator(bool optional) : base(optional) { }

    public override Result Validate(IJsonElement json, string name) {
        if (json.GetElementType() == JsonElementType.BOOLEAN) {
            return Result.Success();
        }
        return Result.Failure(new Message($"Expected {name} to be a boolean"));
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override BooleanJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new BooleanJsonValidator(optional);
        }
    }
}

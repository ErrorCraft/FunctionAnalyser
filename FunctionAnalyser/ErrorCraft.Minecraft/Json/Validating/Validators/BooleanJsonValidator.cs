using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class BooleanJsonValidator : JsonValidator {
    public BooleanJsonValidator(bool optional) : base(optional) { }

    public override Result<IJsonValidated> Validate(IJsonElement json, string name) {
        Result<bool> result = json.AsBoolean(name);
        if (result.Successful) {
            return Result<IJsonValidated>.Success(new ValidatedJsonBoolean(result.Value));
        }
        return Result<IJsonValidated>.Failure(result);
    }

    public class Serialiser : Serialiser<BooleanJsonValidator> {
        public override BooleanJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new BooleanJsonValidator(optional);
        }
    }
}

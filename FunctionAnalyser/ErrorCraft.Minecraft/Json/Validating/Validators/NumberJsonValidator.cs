using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class NumberJsonValidator : JsonValidator {
    public NumberJsonValidator(bool optional) : base(optional) { }

    public override Result<IJsonValidated> Validate(IJsonElement json, string name) {
        Result<double> result = json.AsDouble(name);
        if (result.Successful) {
            return Result<IJsonValidated>.Success(new ValidatedJsonNumber(result.Value));
        }
        return Result<IJsonValidated>.Failure(result);
    }

    public new class Serialiser : JsonValidator.Serialiser {
        public override NumberJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new NumberJsonValidator(optional);
        }
    }
}

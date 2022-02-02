using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating.Validators;

public class StringJsonValidator : JsonValidator {
    public StringJsonValidator(bool optional) : base(optional) { }

    public override Result<IJsonValidated> Validate(IJsonElement json, string name) {
        Result<string> result = json.AsString(name);
        if (result.Successful) {
            return Result<IJsonValidated>.Success(new ValidatedJsonString(result.Value));
        }
        return Result<IJsonValidated>.Failure(result);
    }

    public class Serialiser : Serialiser<StringJsonValidator> {
        public override StringJsonValidator FromJson(JObject json, JsonSerializer serialiser, bool optional) {
            return new StringJsonValidator(optional);
        }
    }
}

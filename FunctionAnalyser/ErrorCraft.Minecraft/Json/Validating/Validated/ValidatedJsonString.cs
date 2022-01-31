namespace ErrorCraft.Minecraft.Json.Validating.Validated;

public class ValidatedJsonString : IJsonValidated {
    private readonly string Value;

    public JsonValidatedType ValidatorType {
        get {
            return JsonValidatedType.STRING;
        }
    }

    public ValidatedJsonString(string value) {
        Value = value;
    }

    public static implicit operator string(ValidatedJsonString value) {
        return value.Value;
    }

    public static implicit operator ValidatedJsonString(string value) {
        return new ValidatedJsonString(value);
    }
}

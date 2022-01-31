namespace ErrorCraft.Minecraft.Json.Validating.Validated;

public class ValidatedJsonBoolean : IJsonValidated {
    private readonly bool Value;

    public JsonValidatedType ValidatorType {
        get {
            return JsonValidatedType.BOOLEAN;
        }
    }

    public ValidatedJsonBoolean(bool value) {
        Value = value;
    }

    public static implicit operator bool(ValidatedJsonBoolean value) {
        return value.Value;
    }

    public static implicit operator ValidatedJsonBoolean(bool value) {
        return new ValidatedJsonBoolean(value);
    }
}

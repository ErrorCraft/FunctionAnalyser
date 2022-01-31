namespace ErrorCraft.Minecraft.Json.Validating.Validated;

public class ValidatedJsonNumber : IJsonValidated {
    private readonly double Value;
    
    public JsonValidatedType ValidatorType {
        get {
            return JsonValidatedType.NUMBER;
        }
    }

    public ValidatedJsonNumber(double value) {
        Value = value;
    }

    public static implicit operator double(ValidatedJsonNumber value) {
        return value.Value;
    }

    public static implicit operator ValidatedJsonNumber(double value) {
        return new ValidatedJsonNumber(value);
    }
}

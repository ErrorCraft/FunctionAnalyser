using ErrorCraft.Minecraft.Json.Validating.Validated;

namespace ErrorCraft.Minecraft.Predicates;

public class Predicate {
    public ValidatedJsonObject Items { get; }

    public Predicate(ValidatedJsonObject items) {
        Items = items;
    }
}

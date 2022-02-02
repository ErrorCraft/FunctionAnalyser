using ErrorCraft.Minecraft.Pack.Resources;
using ErrorCraft.Minecraft.Resources.Json;

namespace ErrorCraft.Minecraft.Pack;

public class PackResources {
    public JsonValidatorTypeCollection? PredicateTypes { get; }

    public PackResources(JsonValidatorTypeCollection? predicateTypes) {
        PredicateTypes = predicateTypes;
    }

    public static class Loader {
        private static readonly OptionalLoadableJsonResource<JsonValidatorTypeCollection> PREDICATE_TYPES_LOADER = JsonValidatorTypeCollection.CreateLoader("predicates.json");

        public static PackResources Load(string path) {
            JsonValidatorTypeCollection? predicateTypes = PREDICATE_TYPES_LOADER.Load(path);
            return new PackResources(predicateTypes);
        }
    }
}

using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Pack.Resources;
using ErrorCraft.Minecraft.Predicates;
using ErrorCraft.Minecraft.Util;

namespace ErrorCraft.Minecraft.Pack.Loading.Loaders;

public class PredicateResourceLoader : JsonResourceLoader<Predicate> {
    public PredicateResourceLoader(string folder, string fileExtension) : base(folder, fileExtension) { }

    protected override Result<Predicate> Apply(IJsonElement json, PackVersion version) {
        JsonValidatorTypeCollection? predicateTypes = version.Resources.PredicateTypes;
        if (predicateTypes == null) {
            return Result<Predicate>.Failure(new Message("Predicate types could not be found"));
        }
        Result<JsonObject> objectResult = json.AsObject("item");
        if (!objectResult.Successful) {
            return Result<Predicate>.Failure(objectResult);
        }
        Result<ValidatedJsonObject> validateResult = predicateTypes.Validate(objectResult.Value);
        return Result<Predicate>.From(validateResult, (value) => new Predicate(value));
    }
}

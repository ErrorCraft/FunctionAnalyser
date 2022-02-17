using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Pack.Resources;
using ErrorCraft.Minecraft.Predicates;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Pack.Loading.Loaders;

public class PredicateResourceLoader : JsonResourceLoader<Predicate> {
    public PredicateResourceLoader(string folder, string fileExtension) : base(folder, fileExtension) { }

    protected override Dictionary<ResourceLocation, Predicate> Apply(Dictionary<ResourceLocation, IJsonElement> prepared, PackVersion version) {
        Dictionary<ResourceLocation, Predicate> predicates = new Dictionary<ResourceLocation, Predicate>();
        JsonValidatorTypeCollection? predicateTypes = version.Resources.PredicateTypes;
        if (predicateTypes == null) {
            return predicates;
        }
        foreach (KeyValuePair<ResourceLocation, IJsonElement> pair in prepared) {
            Result<ValidatedJsonObject> validateResult = predicateTypes.Validate(pair.Value);
            if (validateResult.Successful) {
                Predicate predicate = new Predicate(validateResult.Value);
                predicates.Add(pair.Key, predicate);
            }
        }
        return predicates;
    }
}

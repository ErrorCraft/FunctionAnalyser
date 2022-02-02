using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Util;

namespace ErrorCraft.Minecraft.Pack;

public class PackVersion {
    private readonly PackDefinition Definition;
    private readonly PackMetadata Metadata;

    public PackResources Resources { get; }

    public PackVersion(PackDefinition definition, PackMetadata metadata, PackResources resources) {
        Definition = definition;
        Metadata = metadata;
        Resources = resources;
    }

    public Result<IJsonValidated> Analyse(string path) {
        Result<IJsonValidated> metadataResult = Metadata.Analyse(path);
        return metadataResult;
    }
}

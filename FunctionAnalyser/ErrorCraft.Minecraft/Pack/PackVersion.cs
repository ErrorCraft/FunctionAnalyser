using ErrorCraft.Minecraft.Util;

namespace ErrorCraft.Minecraft.Pack;

public class PackVersion {
    private readonly PackDefinition Definition;
    private readonly PackMetadata Metadata;
    private readonly PackResources Resources;

    public PackVersion(PackDefinition definition, PackMetadata metadata) {
        Definition = definition;
        Metadata = metadata;
        Resources = null!;
    }

    public Result Analyse(string path) {
        Result metadataResult = Metadata.Analyse(path);
        return metadataResult;
    }
}

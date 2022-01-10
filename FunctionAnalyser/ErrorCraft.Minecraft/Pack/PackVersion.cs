namespace ErrorCraft.Minecraft.Pack;

public class PackVersion {
    private readonly PackDefinition Definition;
    private readonly PackMetadata Metadata;

    public PackVersion(PackDefinition definition, PackMetadata metadata) {
        Definition = definition;
        Metadata = metadata;
    }
}

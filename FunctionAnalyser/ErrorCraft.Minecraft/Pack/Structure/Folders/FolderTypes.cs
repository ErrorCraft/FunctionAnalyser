using ErrorCraft.Minecraft.Pack.Structure.Folders.Types;
using ErrorCraft.Minecraft.Util.Registries;
using ErrorCraft.Minecraft.Util.ResourceLocations;

namespace ErrorCraft.Minecraft.Pack.Structure.Folders;

public static class FolderTypes {
    public static readonly Registry<IFolderType> FOLDER_TYPE = new Registry<IFolderType>();

    public static readonly IFolderType RESOURCE = Register("resource", new ResourceFolderType());
    public static readonly IFolderType DIRECT = Register("direct", new DirectFolderType());

    private static IFolderType Register(string id, IFolderType folderType) {
        return FOLDER_TYPE.Register(new ExactResourceLocation(id), folderType);
    }
}

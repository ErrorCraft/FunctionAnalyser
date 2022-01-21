using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Pack.Structure.Folders;

public interface IFolderType {
    IEnumerable<ResourceLocation> FindResources(string basePath, string directory, string extension);
}

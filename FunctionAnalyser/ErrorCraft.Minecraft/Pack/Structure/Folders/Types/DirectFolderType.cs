using ErrorCraft.Minecraft.Util.Extensions;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft.Pack.Structure.Folders.Types;

public class DirectFolderType : IFolderType {
    public IEnumerable<ResourceLocation> FindResources(string basePath, string directory, string extension) {
        string resourceDirectory = Path.Combine(basePath, directory);
        foreach (string file in DirectoryExtensions.GetFiles(resourceDirectory, extension)) {
            yield return new LazyResourceLocation(file);
        }
    }
}

using ErrorCraft.Minecraft.Util.Extensions;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft.Pack.Structure.Folders.Types;

public class ResourceFolderType : IFolderType {
    public IEnumerable<ResourceLocation> FindResources(string basePath, string directory, string extension) {
        foreach (string namespaceDirectory in Directory.GetDirectories(basePath)) {
            string ns = Path.GetFileName(namespaceDirectory);
            string resourceDirectory = Path.Combine(namespaceDirectory, directory);
            foreach (string file in DirectoryExtensions.GetFiles(resourceDirectory, extension)) {
                yield return new ExactResourceLocation(ns, file);
            }
        }
    }
}

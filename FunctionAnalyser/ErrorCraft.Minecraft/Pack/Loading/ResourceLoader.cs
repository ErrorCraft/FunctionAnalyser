using ErrorCraft.Minecraft.Pack.Structure.Folders;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ErrorCraft.Minecraft.Pack.Loading;

public abstract class ResourceLoader<T> {
	private readonly string Folder;
	private readonly string FileExtension;

	protected ResourceLoader(string folder, string fileExtension) {
		Folder = folder;
		FileExtension = fileExtension;
    }

	public async Task<Dictionary<ResourceLocation, T>> LoadAsync(string path, IFolderType folderType, PackVersion version) {
		Dictionary<ResourceLocation, T> items = new Dictionary<ResourceLocation, T>();
		foreach (ResourceLocation resourceLocation in folderType.FindResources(path, Folder, FileExtension)) {
			string itemPath = Path.Combine(path, resourceLocation.Namespace, Folder, resourceLocation.Path) + FileExtension;
			Result<T> result = await Apply(itemPath, version);
			if (result.Successful) {
				items.Add(resourceLocation, result.Value);
			}
		}
		return items;
	}

	protected abstract Task<Result<T>> Apply(string file, PackVersion version);
}

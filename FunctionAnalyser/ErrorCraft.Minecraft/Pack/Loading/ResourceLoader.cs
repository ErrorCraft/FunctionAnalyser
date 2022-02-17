using ErrorCraft.Minecraft.Pack.Structure.Folders;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ErrorCraft.Minecraft.Pack.Loading;

public abstract class ResourceLoader<U, T> {
	private readonly string Folder;
	private readonly string FileExtension;

	protected ResourceLoader(string folder, string fileExtension) {
		Folder = folder;
		FileExtension = fileExtension;
    }

	public async Task<Dictionary<ResourceLocation, T>> LoadAsync(string path, IFolderType folderType, PackVersion version) {
		Dictionary<ResourceLocation, U> prepared = new Dictionary<ResourceLocation, U>();
		foreach (ResourceLocation resourceLocation in folderType.FindResources(path, Folder, FileExtension)) {
			string itemPath = Path.Combine(path, resourceLocation.Namespace, Folder, resourceLocation.Path) + FileExtension;
			Result<U> preparedResult = await Prepare(itemPath);
			if (preparedResult.Successful) {
				prepared.Add(resourceLocation, preparedResult.Value);
			}
		}
		return Apply(prepared, version);
	}

	protected abstract Task<Result<U>> Prepare(string file);
	protected abstract Dictionary<ResourceLocation, T> Apply(Dictionary<ResourceLocation, U> prepared, PackVersion version);
}

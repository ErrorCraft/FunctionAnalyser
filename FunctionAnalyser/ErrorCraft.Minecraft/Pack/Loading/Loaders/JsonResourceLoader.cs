using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using System.Threading.Tasks;

namespace ErrorCraft.Minecraft.Pack.Loading.Loaders;

public abstract class JsonResourceLoader<T> : ResourceLoader<IJsonElement, T> {
    protected JsonResourceLoader(string folder, string fileExtension) : base(folder, fileExtension) { }

    protected override async Task<Result<IJsonElement>> Prepare(string file) {
        return await JsonReader.ReadFromFileAsync(file);
    }
}

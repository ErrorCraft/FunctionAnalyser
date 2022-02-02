using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using System.Threading.Tasks;

namespace ErrorCraft.Minecraft.Pack.Loading.Loaders;

public abstract class JsonResourceLoader<T> : ResourceLoader<T> {
    protected JsonResourceLoader(string folder, string fileExtension) : base(folder, fileExtension) { }

    protected override async Task<Result<T>> Apply(string file, PackVersion version) {
        Result<IJsonElement> jsonResult = await JsonReader.ReadFromFileAsync(file);
        if (!jsonResult.Successful) {
            return Result<T>.Failure(jsonResult);
        }
        return Apply(jsonResult.Value, version);
    }

    protected abstract Result<T> Apply(IJsonElement json, PackVersion version);
}

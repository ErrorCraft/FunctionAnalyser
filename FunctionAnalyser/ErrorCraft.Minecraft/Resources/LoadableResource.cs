using System.IO;

namespace ErrorCraft.Minecraft.Resources;

public abstract class LoadableResource<T> {
    protected readonly string FileName;

    protected LoadableResource(string fileName) {
        FileName = fileName;
    }

    protected abstract T Apply(string fileContents);

    protected virtual T Fail() {
        throw new LoadableResourceException(FileName);
    }

    public T Load(string path) {
        string file = Path.Combine(path, FileName);
        if (File.Exists(file)) {
            return Read(file);
        }
        return Fail();
    }

    private T Read(string file) {
        string fileContents = File.ReadAllText(file);
        return Apply(fileContents);
    }
}

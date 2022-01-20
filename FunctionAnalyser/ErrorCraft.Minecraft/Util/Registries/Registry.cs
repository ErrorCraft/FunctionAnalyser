using ErrorCraft.Minecraft.Util.ResourceLocations;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Util.Registries;

public class Registry<T> {
    private readonly Dictionary<ExactResourceLocation, T> Items = new Dictionary<ExactResourceLocation, T>();

    public T this[ExactResourceLocation resourceLocation] {
        get { return Items[resourceLocation]; }
    }

    public T Register(ExactResourceLocation resourceLocation, T value) {
        Items[resourceLocation] = value;
        return value;
    }
}

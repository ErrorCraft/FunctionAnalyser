using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Util;

public class Registry<T> {
    private readonly Dictionary<ResourceLocation, T> Items = new Dictionary<ResourceLocation, T>();

    public T this[ResourceLocation resourceLocation] {
        get { return Items[resourceLocation]; }
    }

    public T Register(ResourceLocation resourceLocation, T value) {
        Items[resourceLocation] = value;
        return value;
    }
}

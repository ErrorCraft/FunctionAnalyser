using System;

namespace ErrorCraft.Minecraft.Util.ResourceLocations;

public abstract class ResourceLocation : IEquatable<ResourceLocation> {
    protected const char NAMESPACE_SEPARATOR = ':';
    protected const string DEFAULT_NAMESPACE = "minecraft";

    public string Namespace { get; }
    public string Path { get; }

    protected ResourceLocation(string path) : this(DEFAULT_NAMESPACE, path) { }

    protected ResourceLocation(string ns, string path) {
        Namespace = ns;
        Path = path;
    }

    public bool Equals(ResourceLocation? other) {
        return other != null
            && Namespace == other.Namespace
            && Path == other.Path;
    }

    public override bool Equals(object? obj) {
        return Equals(obj as ResourceLocation);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Namespace, Path);
    }

    public override string ToString() {
        return $"{Namespace}{NAMESPACE_SEPARATOR}{Path}";
    }
}

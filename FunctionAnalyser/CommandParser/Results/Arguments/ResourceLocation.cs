using System;

namespace CommandParser.Results.Arguments
{
    public class ResourceLocation
    {
        public static readonly ResourceLocation PLAYER_ENTITY = new ResourceLocation("player");
        public const char NAMESPACE_SEPARATOR = ':';
        public const char TAG_CHARACTER = '#';
        private const string DEFAULT_NAMESPACE = "minecraft";

        public string Resource { get; }
        public string Path { get; }
        public bool IsTag { get; }

        public ResourceLocation(string path, string resource = DEFAULT_NAMESPACE)
        {
            Path = path;
            Resource = resource;
            IsTag = false;
        }

        public ResourceLocation(ResourceLocation other, bool isTag)
        {
            Resource = other.Resource;
            Path = other.Path;
            IsTag = isTag;
        }

        public override string ToString()
        {
            if (IsTag) return $"{TAG_CHARACTER}{Resource}{NAMESPACE_SEPARATOR}{Path}";
            else return $"{Resource}{NAMESPACE_SEPARATOR}{Path}";
        }

        public bool IsDefaultNamespace()
        {
            return Resource == DEFAULT_NAMESPACE;
        }

        public override bool Equals(object obj)
        {
            return obj is ResourceLocation result &&
                   Resource == result.Resource &&
                   Path == result.Path &&
                   IsTag == result.IsTag;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Resource, Path, IsTag);
        }
    }
}

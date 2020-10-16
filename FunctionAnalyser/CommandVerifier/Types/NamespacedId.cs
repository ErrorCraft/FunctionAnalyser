using System;
using System.Text.RegularExpressions;

namespace CommandVerifier.Types
{
    public class NamespacedId
    {
        public string Namespace { get; }
        public string Path { get; }
        public bool IsTag { get; }
        private static readonly char NAMESPACE_SEPARATOR = ':';
        private static readonly char TAG_CHARACTER = '#';
        private static readonly string DEFAULT_NAMESPACE = "minecraft";
        private static readonly Regex NAMESPACED_ID_REGEX = new Regex("^#?[a-z0-9._-]*:?[a-z0-9._/-]*$");
        public static NamespacedId PLAYER_ENTITY { get; } = new NamespacedId("player", false);

        private NamespacedId(string @namespace, string path, bool isTag)
        {
            Namespace = @namespace;
            Path = path;
            IsTag = isTag;
        }

        private NamespacedId(string Path, bool IsTag)
            : this(DEFAULT_NAMESPACE, Path, IsTag) { }

        public static bool TryParse(string s, out NamespacedId result)
        {
            result = null;
            if (!NAMESPACED_ID_REGEX.IsMatch(s)) return false;

            bool isTag = false;
            string namespacedId = s;
            
            if (namespacedId.StartsWith(TAG_CHARACTER))
            {
                isTag = true;
                namespacedId = namespacedId.Substring(1);
            }
            string[] values = namespacedId.Split(NAMESPACE_SEPARATOR);
            if (values.Length > 2) return false;
            if (values.Length == 2)
            {
                if (string.IsNullOrEmpty(values[0])) result = new NamespacedId(values[1], isTag);
                else result = new NamespacedId(values[0], values[1], isTag);
            }
            else result = new NamespacedId(values[0], isTag);
            return true;
        }

        public bool IsDefaultNamespace() => Namespace == DEFAULT_NAMESPACE;

        public override string ToString() => (IsTag ? TAG_CHARACTER.ToString() : "") + Namespace + NAMESPACE_SEPARATOR + Path;

        public override bool Equals(object obj)
        {
            return obj is NamespacedId id &&
                   Namespace == id.Namespace &&
                   Path == id.Path &&
                   IsTag == id.IsTag;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Namespace, Path, IsTag);
        }
    }
}

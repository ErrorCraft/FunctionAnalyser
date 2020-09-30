using CommandVerifier.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandVerifier.Types
{
    public class NamespacedId
    {
        public readonly string Namespace;
        public readonly string Path;
        public readonly bool IsTag;
        private static readonly char NAMESPACE_SEPARATOR = ':';
        private static readonly char TAG_CHARACTER = '#';
        private static readonly string DEFAULT_NAMESPACE = "minecraft";
        private static readonly Regex NAMESPACE_REGEX = new Regex("^[a-z0-9._-]*$");

        public NamespacedId(string Namespace, string Path, bool IsTag)
        {
            this.Namespace = Namespace;
            this.Path = Path;
            this.IsTag = IsTag;
        }

        public NamespacedId(string Path, bool IsTag)
            : this(DEFAULT_NAMESPACE, Path, IsTag) { }

        public static bool TryParse(string s, bool is_tag, out NamespacedId result)
        {
            result = null;
            string[] values = s.Split(NAMESPACE_SEPARATOR);
            if (values.Length > 2) return false;
            if (values.Length == 2)
            {
                if (string.IsNullOrEmpty(values[0])) result = new NamespacedId(values[1], is_tag);
                else if (!NAMESPACE_REGEX.IsMatch(values[0])) return false;
                else result = new NamespacedId(values[0], values[1], is_tag);
            }
            else result = new NamespacedId(values[0], is_tag);
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

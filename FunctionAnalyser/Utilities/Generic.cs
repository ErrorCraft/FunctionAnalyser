using System;
using System.Reflection;

namespace Utilities
{
    public static class Generic
    {
        public static string CombinePaths(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1)) return path2;
            else if (string.IsNullOrEmpty(path2)) return path1;
            else if (path1.EndsWith('/')) return path1 + path2;
            else return path1 + '/' + path2;
        }

        internal static TAttribute GetAttribute<TEnum, TAttribute>(TEnum value) where TEnum : struct, Enum where TAttribute : Attribute
        {
            FieldInfo field = typeof(TEnum).GetField(value.ToString());
            return field?.GetCustomAttribute<TAttribute>();
        }
    }
}

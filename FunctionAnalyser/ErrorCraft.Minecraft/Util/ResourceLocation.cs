using System;
using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Util;

public class ResourceLocation : IEquatable<ResourceLocation> {
    private const char NAMESPACE_SEPARATOR = ':';
    private const string DEFAULT_NAMESPACE = "minecraft";

    public string Namespace { get; }
    public string Path { get; }

    public ResourceLocation(string path) : this(DEFAULT_NAMESPACE, path) { }

    public ResourceLocation(string ns, string path) {
        Namespace = IsValidNamespace(ns) ? ns : throw new ArgumentException($"Invalid namespace for resource location: '{ns}'");
        Path = IsValidPath(path) ? path : throw new ArgumentException($"Invalid path for resource location: '{path}");
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

    public static bool TryParse(string input, [NotNullWhen(true)] out ResourceLocation? result) {
        result = null;
        string[] items = Split(input);
        if (!IsValidNamespace(items[0])) {
            return false;
        }
        if (!IsValidPath(items[1])) {
            return false;
        }
        result = new ResourceLocation(items[0], items[1]);
        return true;
    }

    private static string[] Split(string input) {
        string[] result = new string[2] { DEFAULT_NAMESPACE, input };
        int i = input.IndexOf(NAMESPACE_SEPARATOR);
        if (i > -1) {
            result[1] = input[(i + 1)..];
            if (i > 0) {
                result[0] = input[0..i];
            }
        }
        return result;
    }

    private static bool IsValidNamespace(string input) {
        for (int i = input.Length - 1; i >= 0; i--) {
            if (!IsValidNamespaceCharacter(input[i])) {
                return false;
            }
        }
        return true;
    }

    private static bool IsValidPath(string input) {
        for (int i = input.Length - 1; i >= 0; i--) {
            if (!IsValidPathCharacter(input[i])) {
                return false;
            }
        }
        return true;
    }

    private static bool IsValidNamespaceCharacter(char c) {
        return c >= '0' && c <= '9'
            || c >= 'a' && c <= 'z'
            || c == '_' || c == '-' || c == '.';
    }

    private static bool IsValidPathCharacter(char c) {
        return IsValidNamespaceCharacter(c) || c == '/';
    }
}

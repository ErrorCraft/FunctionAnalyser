using System;
using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Util.ResourceLocations;

public class ExactResourceLocation : ResourceLocation {
    public ExactResourceLocation(string path) : base(DEFAULT_NAMESPACE, path) { }

    public ExactResourceLocation(string ns, string path) : base(ns, path) {
        if (!IsValidNamespace(ns)) {
            throw new ArgumentException($"Invalid namespace for resource location: '{ns}'");
        }
        if (!IsValidPath(path)) {
            throw new ArgumentException($"Invalid path for resource location: '{path}");
        }
    }

    public static bool TryParse(string input, [NotNullWhen(true)] out ExactResourceLocation? result) {
        result = null;
        string[] items = Split(input);
        if (!IsValidNamespace(items[0])) {
            return false;
        }
        if (!IsValidPath(items[1])) {
            return false;
        }
        result = new ExactResourceLocation(items[0], items[1]);
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

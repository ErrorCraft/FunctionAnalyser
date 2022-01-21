using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft.Util.Extensions;

internal static class DirectoryExtensions {
    public static IEnumerable<string> GetFiles(string directory, string extension) {
        if (!Directory.Exists(directory)) {
            yield break;
        }

        int directoryLength = GetDirectoryLength(directory);

        Queue<string> directories = new Queue<string>();
        directories.Enqueue(directory);

        while (directories.TryDequeue(out string? currentDirectory)) {
            directories.Enqueue(Directory.EnumerateDirectories(currentDirectory));
            foreach (string file in Directory.EnumerateFiles(currentDirectory)) {
                if (file.EndsWith(extension)) {
                    yield return file[directoryLength..].Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
            }
        }
    }

    public static int GetDirectoryLength(string directory) {
        int length = directory.Length;
        if (Path.EndsInDirectorySeparator(directory)) {
            return length;
        }
        return length + 1;
    }
}

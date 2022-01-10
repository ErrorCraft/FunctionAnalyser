using System;

namespace ErrorCraft.Minecraft.Resources;

public class LoadableResourceException : Exception {
    public string FileName { get; }

    public LoadableResourceException(string fileName) : this(fileName, $"Unable to load '{fileName}'") { }

    public LoadableResourceException(string fileName, string? message) : base(message) {
        FileName = fileName;
    }

    public LoadableResourceException(string fileName, string? message, Exception? innerException) : base(message, innerException) {
        FileName = fileName;
    }
}

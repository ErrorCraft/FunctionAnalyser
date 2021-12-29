using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft;

public class GameVersionCollection {
    private const string VERSION_FILE = "version.json";

    private readonly Dictionary<string, GameVersion> Versions;

    private GameVersionCollection(Dictionary<string, GameVersion> versions) {
        Versions = versions;
    }

    public static GameVersionCollection LoadVersions(string path) {
        Dictionary<string, GameVersion> versions = new Dictionary<string, GameVersion>();

        string[] directories = Directory.GetDirectories(path);
        foreach (string dir in directories) {
            string name = Path.GetFileName(dir);
            GameVersion? version = GetVersion(dir);
            if (version != null) {
                versions.Add(name, version);
            }
        }

        return new GameVersionCollection(versions);
    }

    private static GameVersion? GetVersion(string path) {
        string versionFile = Path.Combine(path, VERSION_FILE);
        if (!File.Exists(versionFile)) {
            return null;
        }
        return new GameVersion();
    }
}

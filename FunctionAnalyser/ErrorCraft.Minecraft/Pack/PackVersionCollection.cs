using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft.Pack;

public class PackVersionCollection {
    private const string VERSION_FILE = "version.json";

    private readonly Dictionary<string, PackVersion> Versions;

    private PackVersionCollection(Dictionary<string, PackVersion> versions) {
        Versions = versions;
    }

    public void Write() {
        Console.WriteLine("Versions:");
        foreach (KeyValuePair<string, PackVersion> pair in Versions) {
            Console.WriteLine($"{pair.Key}");
        }
    }

    public static PackVersionCollection LoadVersions(string path) {
        Dictionary<string, PackVersion> versions = new Dictionary<string, PackVersion>();

        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories) {
            string name = Path.GetFileName(directory);
            PackVersion? version = GetVersion(directory);
            if (version != null) {
                versions.Add(name, version);
            }
        }

        return new PackVersionCollection(versions);
    }

    private static PackVersion? GetVersion(string path) {
        string versionFile = Path.Combine(path, VERSION_FILE);
        if (!File.Exists(versionFile)) {
            return null;
        }
        string json = File.ReadAllText(versionFile);
        PackVersion packVersion = JsonConvert.DeserializeObject<PackVersion>(json, new PackVersion.Serialiser(), new PackRoot.Serialiser())!;
        return packVersion;
    }
}

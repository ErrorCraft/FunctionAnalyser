using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Resources.Json;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ErrorCraft.Minecraft.Pack;

public class PackVersionCollection {
    private const string VERSION_FILE = "version.json";
    private const string RESOURCES_DIRECTORY = "resources";
    private static readonly RequiredLoadableJsonResource<PackMetadata> METADATA_LOADER = new RequiredLoadableJsonResource<PackMetadata>("metadata.json", new JsonSerialiserConverter<PackMetadata>(new PackMetadata.Serialiser()), new JsonSerialiserConverter<JsonValidator>(JsonValidatorTypes.CreateJsonSerialiser()));

    private readonly Dictionary<string, PackVersion> Versions;

    private PackVersionCollection(Dictionary<string, PackVersion> versions) {
        Versions = versions;
    }

    public PackVersion this[string name] {
        get { return Versions[name]; }
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
        PackDefinition packDefinition = JsonConvert.DeserializeObject<PackDefinition>(json, new JsonSerialiserConverter<PackDefinition>(new PackDefinition.Serialiser()))!;
        PackMetadata metadata = METADATA_LOADER.Load(path);
        string resourcesPath = Path.Combine(path, RESOURCES_DIRECTORY);
        PackResources resources = PackResources.Loader.Load(resourcesPath);
        return new PackVersion(packDefinition, metadata, resources);
    }
}

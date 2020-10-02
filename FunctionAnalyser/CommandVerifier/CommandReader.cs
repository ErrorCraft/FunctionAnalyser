using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CommandVerifier.Commands;

namespace CommandVerifier
{
    public class CommandReader
    {
        private static Dictionary<string, CommandCollection> CommandCollections;

        static CommandReader()
        {
            CommandCollections = new Dictionary<string, CommandCollection>();
        }

        public static void SetCommands(string json)
        {
            CommandCollections = JsonConvert.DeserializeObject<Dictionary<string, CommandCollection>>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }

        public CommandReader() { }

        public bool Parse(string version, StringReader reader)
        {
            if (!CommandCollections.ContainsKey(version)) return false;
            return CommandCollections[version].Parse(reader);
        }

        public static string GetFancyName(string version)
        {
            return CommandCollections[version].Name;
        }
    }
}

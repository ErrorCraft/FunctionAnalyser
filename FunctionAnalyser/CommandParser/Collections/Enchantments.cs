﻿using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Enchantments
    {
        private static HashSet<string> Options = new HashSet<string>();

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(ResourceLocation enchantment)
        {
            return enchantment.IsDefaultNamespace() && Options.Contains(enchantment.Path);
        }
    }
}
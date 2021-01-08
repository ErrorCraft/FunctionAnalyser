﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Sorts
    {
        private static HashSet<string> Options = new HashSet<string>();
        private readonly HashSet<string> Values;

        public Sorts(HashSet<string> values)
        {
            Values = values;
        }

        public static void Set(string json)
        {
            Options = JsonConvert.DeserializeObject<HashSet<string>>(json);
        }

        public static bool Contains(string gamemode)
        {
            return Options.Contains(gamemode);
        }
    }
}

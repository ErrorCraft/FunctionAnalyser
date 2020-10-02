using AdvancedText;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface
{
    public class Version
    {
        private Version() { }

        [JsonProperty("version")]
        public static int ProgramVersion { get; set; }

        [JsonProperty("description")]
        public static TextComponent[] Description { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class Particles
    {
        public static Dictionary<string, Particle> Options { get; private set; }

        static Particles()
        {
            Options = new Dictionary<string, Particle>();
        }

        public static void SetOptions(string json)
        {
            Options = JsonConvert.DeserializeObject<Dictionary<string, Particle>>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }
    }
}

using CommandVerifier.Commands.Converters;
using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json;

namespace CommandVerifier.Commands.Collections
{
    public class Particle
    {
        public bool HasParameters { get { return Parameters != null && Parameters.Length > 0; } }

        [JsonConstructor]
        public Particle()
        {
            Parameters = new Subcommand[0];
        }

        [JsonProperty("parameters")]
        [JsonConverter(typeof(SubcommandConverter))]
        public Subcommand[] Parameters { get; set; }
    }
}

using CommandVerifier.Commands.Converters;
using Newtonsoft.Json;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class SubcommandCollection : Subcommand
    {
        [JsonConverter(typeof(SubcommandConverter))]
        [JsonProperty("values", Required = Required.Always)]
        public Subcommand[] Values { get; set; }
    }
}

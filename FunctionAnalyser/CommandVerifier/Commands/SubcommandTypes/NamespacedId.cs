using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class NamespacedId : Subcommand
    {
        public enum FromType
        {
            [EnumMember(Value = "none")]
            None = 0,
            [EnumMember(Value = "predicate")]
            Predicate = 1
        }

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(FromType.None)]
        public FromType From { get; set; }

        [JsonProperty("disable_tags")]
        [DefaultValue(false)]
        public bool DisableTags { get; set; }
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            if (reader.TryReadNamespacedId(throw_on_fail, DisableTags, out _))
            {
                SetLoopAttributes(reader);
                if (From == FromType.Predicate) reader.Information.PredicateCalls++;
                return true;
            }
            return false;
        }
    }
}

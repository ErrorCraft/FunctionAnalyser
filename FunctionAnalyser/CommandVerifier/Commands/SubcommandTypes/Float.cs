using CommandVerifier.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Float : Subcommand
    {
        [JsonProperty("minimum")]
        [DefaultValue(float.NegativeInfinity)]
        public float Minimum { get; set; }

        [JsonProperty("maximum")]
        [DefaultValue(float.PositiveInfinity)]
        public float Maximum { get; set; }

        [JsonProperty("disable_infinity")]
        [DefaultValue(false)]
        public bool DisableInfinity { get; set; }

        [OnDeserialized]
        internal void Validate(StreamingContext context)
        {
            if (DisableInfinity)
            {
                if (float.IsNegativeInfinity(Minimum)) Minimum = float.MinValue;
                if (float.IsPositiveInfinity(Maximum)) Maximum = float.MaxValue;
            }
            if (Minimum > Maximum) throw new MinimumLargerThanMaximumException();
        }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }
            int start = reader.Cursor;
            if (reader.TryReadFloat(throw_on_fail, out float result))
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.FloatTooLow(result, Minimum).AddWithContext(reader);
                    return false;
                }
                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.FloatTooHigh(result, Maximum).AddWithContext(reader);
                    return false;
                }
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}

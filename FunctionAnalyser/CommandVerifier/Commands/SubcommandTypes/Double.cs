using CommandVerifier.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Double : Subcommand
    {
        [JsonProperty("minimum")]
        [DefaultValue(double.NegativeInfinity)]
        public double Minimum { get; set; }

        [JsonProperty("maximum")]
        [DefaultValue(double.PositiveInfinity)]
        public double Maximum { get; set; }

        [JsonProperty("disable_infinity")]
        [DefaultValue(false)]
        public bool DisableInfinity { get; set; }

        [OnDeserialized]
        internal void Validate(StreamingContext context)
        {
            if (DisableInfinity)
            {
                if (double.IsNegativeInfinity(Minimum)) Minimum = double.MinValue;
                if (double.IsPositiveInfinity(Maximum)) Maximum = double.MaxValue;
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
            if (reader.TryReadDouble(throw_on_fail, out double result))
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.DoubleTooLow(result, Minimum).AddWithContext(reader);
                    return false;
                }
                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.DoubleTooHigh(result, Maximum).AddWithContext(reader);
                    return false;
                }
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}

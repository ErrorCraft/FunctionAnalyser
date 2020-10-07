using CommandVerifier.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Integer : Subcommand
    {
        [JsonProperty("minimum")]
        [DefaultValue(int.MinValue)]
        public int Minimum { get; set; }

        [JsonProperty("maximum")]
        [DefaultValue(int.MaxValue)]
        public int Maximum { get; set; }

        [OnDeserialized]
        internal void Validate(StreamingContext context)
        {
            if (Minimum > Maximum) throw new MinimumLargerThanMaximumException();
        }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            int start = reader.Cursor;
            if (reader.TryReadInt(throw_on_fail, out int result))
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.IntegerTooLow(result, Minimum).AddWithContext(reader);
                    return false;
                }
                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.IntegerTooHigh(result, Maximum).AddWithContext(reader);
                    return false;
                }
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}

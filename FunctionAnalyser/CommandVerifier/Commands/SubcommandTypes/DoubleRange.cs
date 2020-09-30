using CommandVerifier.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class DoubleRange : Subcommand
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
            if (!reader.CanRead())
            {
                if (Optional)
                {
                    reader.commandData.EndedOptional = true;
                    return true;
                }

                if (throw_on_fail) CommandError.ExpectedValueOrRange().AddWithContext(reader);
                return false;
            }
            int start = reader.Cursor;

            double? right;
            if (!TryReadNumber(reader, throw_on_fail, out double? left)) return false;

            // Starts with range
            if (reader.CanRead(2) && reader.Peek() == '.' && reader.Peek(1) == '.')
            {
                reader.Skip(2);
                if (!TryReadNumber(reader, throw_on_fail, out right)) return false;
                if (left == null && right == null)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.ExpectedValueOrRange().AddWithContext(reader);
                    return false;
                }
            }
            else
            {
                right = left;
            }

            if (left == null && right == null)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.ExpectedValueOrRange().AddWithContext(reader);
                return false;
            }

            if (left == null) left = Minimum;
            if (right == null) right = Maximum;

            // Minimum cannot be greater than maximum
            if (left > right)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.RangeMinBiggerThanMax().AddWithContext(reader);
                return false;
            }

            // Only needs two checks, since right cannot be greater than left
            if (left < Minimum)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.RangeDoubleTooLow(Minimum).AddWithContext(reader);
                return false;
            }
            if (right > Maximum)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.RangeDoubleTooHigh(Maximum).AddWithContext(reader);
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }

        private static bool TryReadNumber(StringReader reader, bool may_throw, out double? result)
        {
            result = null;
            int start = reader.Cursor;
            while (reader.CanRead() && IsAllowedInput(reader)) reader.Skip();

            string input = reader.Command[start..reader.Cursor];
            if (string.IsNullOrEmpty(input)) return true;

            if (!double.TryParse(input, out double number))
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.InvalidDouble(input).AddWithContext(reader);
                return false;
            }
            result = number;
            return true;
        }

        private static bool IsAllowedInput(StringReader reader)
        {
            char c = reader.Peek();
            if (c >= '0' && c <= '9' || c == '-') return true;
            if (c == '.') return !reader.CanRead(2) || reader.Peek(1) != '.';
            return false;
        }
    }
}

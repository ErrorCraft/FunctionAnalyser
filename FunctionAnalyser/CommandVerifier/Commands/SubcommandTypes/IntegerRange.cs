using CommandVerifier.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class IntegerRange : Subcommand
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
            if (!reader.CanRead())
            {
                if (Optional)
                {
                    reader.Data.EndedOptional = true;
                    return true;
                }

                if (throw_on_fail) CommandError.ExpectedValueOrRange().AddWithContext(reader);
                return false;
            }
            int start = reader.Cursor;

            int? right;
            if (!TryReadNumber(reader, throw_on_fail, out int? left)) return false;

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
            } else
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
                if (throw_on_fail) CommandError.RangeIntegerTooLow(Minimum).AddWithContext(reader);
                return false;
            }
            if (right > Maximum)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.RangeIntegerTooHigh(Maximum).AddWithContext(reader);
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }

        private static bool TryReadNumber(StringReader reader, bool may_throw, out int? result)
        {
            result = null;
            int start = reader.Cursor;
            while (reader.CanRead() && IsAllowedInput(reader)) reader.Skip();

            string input = reader.Command[start..reader.Cursor];
            if (string.IsNullOrEmpty(input)) return true;

            if (!int.TryParse(input, out int number))
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.InvalidInteger(input).AddWithContext(reader); // Only whole numbers allowed, not decimals? SEE IN-GAME (use something like 1.2.3)
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

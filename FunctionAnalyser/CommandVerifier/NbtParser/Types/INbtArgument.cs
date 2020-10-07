using System.Globalization;
using System.Text.RegularExpressions;

namespace CommandVerifier.NbtParser.Types
{
    public interface INbtArgument
    {
        public static NumberFormatInfo NbtNumberFormatInfo { get; } = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "+",
            NaNSymbol = "NaN"
        };
        public static NumberStyles NbtNumberStyles { get; } = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent;

        public string Get();
        public string Id { get; }

        private static readonly Regex UNQUOTED_NAME_REGEX = new Regex("^[a-zA-Z0-9.+_-]*$");
        public static string TryQuote(string input)
        {
            if (input.Contains('\'') || !UNQUOTED_NAME_REGEX.IsMatch(input)) return "\"" + Escape(input, '"') + "\"";
            else if (input.Contains('"')) return "'" + Escape(input, '\'') + "'";
            return input;
        }

        public static string Escape(string input, char escapingCharacter)
        {
            string s = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == escapingCharacter || input[i] == '\\') s += "\\";
                s += input[i];
            }

            return s;
        }
    }
}

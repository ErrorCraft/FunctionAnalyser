using System.Globalization;
using System.Text.RegularExpressions;

namespace CommandVerifier.NbtParser.Types
{
    public interface NbtArgument
    {
        public static NumberFormatInfo NbtNumberFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "+",
            NaNSymbol = "NaN"
        };
        public static readonly NumberStyles NbtNumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent;

        public string Get();
        public string Id { get; }

        private static readonly Regex characters = new Regex("^[a-zA-Z0-9.+_-]*$");
        public static string TryQuote(string input)
        {
            if (input.Contains('\'') || !characters.IsMatch(input)) return "\"" + Escape(input, '"') + "\"";
            else if (input.Contains('"')) return "'" + Escape(input, '\'') + "'";
            return input;
        }

        public static string Escape(string input, char escaping_character)
        {
            string s = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == escaping_character || input[i] == '\\') s += "\\";
                s += input[i];
            }

            return s;
        }
    }
}

using System.Globalization;

namespace CommandParser.Parsers.NbtParser
{
    public static class NbtNumberProvider
    {
        public static readonly NumberFormatInfo NumberFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "+",
            NaNSymbol = "NaN"
        };
        public static readonly NumberStyles NumberStylesInteger = NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent;
        public static readonly NumberStyles NumberStylesFloating = NumberStylesInteger | NumberStyles.AllowDecimalPoint;
    }
}

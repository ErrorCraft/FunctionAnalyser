using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CommandParser.Minecraft.Nbt {
    internal static class NbtUtilities {
        public static readonly NumberFormatInfo NbtNumberFormatInfo = new NumberFormatInfo() {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "+",
            NaNSymbol = "NaN"
        };
        public static readonly NumberStyles NbtNumberStylesInteger = NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent;
        public static readonly NumberStyles NbtNumberStylesFloating = NbtNumberStylesInteger | NumberStyles.AllowDecimalPoint;

        public static string Join<T>(string separator, IEnumerable<T> collection, Func<T, string> function) {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (T item in collection) {
                stringBuilder.Append(function(item));
                stringBuilder.Append(separator);
            }
            stringBuilder.Length -= separator.Length;
            return stringBuilder.ToString();
        }
    }
}

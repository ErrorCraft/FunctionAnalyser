﻿using System.Globalization;

namespace CommandParser.Providers
{
    public static class NumberProvider
    {
        public static readonly NumberFormatInfo NumberFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "",
            NaNSymbol = "NaN"
        };
        public static readonly NumberStyles NumberStylesInteger = NumberStyles.AllowLeadingSign;
        public static readonly NumberStyles NumberStylesFloating = NumberStylesInteger | NumberStyles.AllowDecimalPoint;
    }
}

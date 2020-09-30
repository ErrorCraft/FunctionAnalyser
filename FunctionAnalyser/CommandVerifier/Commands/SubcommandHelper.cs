using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CommandVerifier.Commands
{
    static class SubcommandHelper
    {
        public static NumberFormatInfo MinecraftNumberFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            PositiveInfinitySymbol = "Infinity",
            NegativeInfinitySymbol = "-Infinity",
            NegativeSign = "-",
            PositiveSign = "",
            NaNSymbol = "NaN"
        };
        public static readonly NumberStyles MinecraftNumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
    }
}

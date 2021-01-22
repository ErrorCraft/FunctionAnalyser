using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Generic;

namespace Utilities
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T value) where T : struct, Enum
        {
            DisplayAttribute displayAttribute = GetAttribute<T, DisplayAttribute>(value);
            return displayAttribute?.Name ?? value.ToString();
        }
    }
}

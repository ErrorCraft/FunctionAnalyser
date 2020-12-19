using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UserInterface
{
    public static class Extensions
    {
        public static string GetDisplayName<T>(this T value) where T : struct, Enum
        {
            DisplayAttribute displayAttribute = GetAttribute<T, DisplayAttribute>(value);
            return displayAttribute?.Name ?? value.ToString();
        }

        private static TAttribute GetAttribute<TEnum, TAttribute>(TEnum value) where TEnum : struct, Enum where TAttribute : Attribute
        {
            FieldInfo field = typeof(TEnum).GetField(value.ToString());
            return field?.GetCustomAttribute<TAttribute>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AdvancedText
{
    public class Colour
    {
        public static class BuiltinColours
        {
            public static readonly Colour DARK_BLUE = new Colour(0, 0, 170);
            public static readonly Colour DARK_GREEN = new Colour(0, 170, 0);
            public static readonly Colour DARK_AQUA = new Colour(0, 170, 170);
            public static readonly Colour DARK_RED = new Colour(170, 0, 0);
            public static readonly Colour DARK_PURPLE = new Colour(170, 0, 170);
            public static readonly Colour GOLD = new Colour(255, 170, 0);
            public static readonly Colour GREY = new Colour(170, 170, 170);
            public static readonly Colour DARK_GREY = new Colour(85, 85, 85);
            public static readonly Colour BLUE = new Colour(85, 85, 255);
            public static readonly Colour GREEN = new Colour(85, 255, 85);
            public static readonly Colour AQUA = new Colour(85, 255, 255);
            public static readonly Colour RED = new Colour(255, 85, 85);
            public static readonly Colour LIGHT_PURPLE = new Colour(255, 85, 255);
            public static readonly Colour YELLOW = new Colour(255, 255, 85);
            public static readonly Colour WHITE = new Colour(255, 255, 255);
            public static readonly Colour BLACK = new Colour(0, 0, 0);
        }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public Colour(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static implicit operator Colour(string s)
        {
            return s switch
            {
                "dark_blue" => BuiltinColours.DARK_BLUE,
                "dark_green" => BuiltinColours.DARK_GREEN,
                "dark_aqua" => BuiltinColours.DARK_AQUA,
                "dark_red" => BuiltinColours.DARK_RED,
                "dark_purple" => BuiltinColours.DARK_PURPLE,
                "gold" => BuiltinColours.GOLD,
                "grey" => BuiltinColours.GREY,
                "dark_grey" => BuiltinColours.DARK_GREY,
                "blue" => BuiltinColours.BLUE,
                "green" => BuiltinColours.GREEN,
                "aqua" => BuiltinColours.AQUA,
                "red" => BuiltinColours.RED,
                "light_purple" => BuiltinColours.LIGHT_PURPLE,
                "yellow" => BuiltinColours.YELLOW,
                "white" => BuiltinColours.WHITE,
                "black" => BuiltinColours.BLACK,
                _ => TextComponent.DefaultColour
            };
        }
    }
}

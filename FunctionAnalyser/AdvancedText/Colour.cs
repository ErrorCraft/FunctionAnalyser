namespace AdvancedText
{
    public class Colour
    {
        public static class BuiltinColours
        {
            public static Colour BLACK { get; } = new Colour(0, 0, 0);
            public static Colour DARK_BLUE { get; } = new Colour(0, 0, 170);
            public static Colour DARK_GREEN { get; } = new Colour(0, 170, 0);
            public static Colour DARK_AQUA { get; } = new Colour(0, 170, 170);
            public static Colour DARK_RED { get; } = new Colour(170, 0, 0);
            public static Colour DARK_PURPLE { get; } = new Colour(170, 0, 170);
            public static Colour GOLD { get; } = new Colour(255, 170, 0);
            public static Colour GREY { get; } = new Colour(170, 170, 170);
            public static Colour DARK_GREY { get; } = new Colour(85, 85, 85);
            public static Colour BLUE { get; } = new Colour(85, 85, 255);
            public static Colour GREEN { get; } = new Colour(85, 255, 85);
            public static Colour AQUA { get; } = new Colour(85, 255, 255);
            public static Colour RED { get; } = new Colour(255, 85, 85);
            public static Colour LIGHT_PURPLE { get; } = new Colour(255, 85, 255);
            public static Colour YELLOW { get; } = new Colour(255, 255, 85);
            public static Colour WHITE { get; } = new Colour(255, 255, 255);
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
    }
}

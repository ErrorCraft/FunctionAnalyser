namespace AdvancedText
{
    public class TextComponent
    {
        public static Colour DefaultColour { get; private set; } = Colour.BuiltinColours.BLACK;
        public string Text { get; set; }
        public Colour Colour { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }

        public TextComponent()
        {
            Text = "";
            Colour = DefaultColour;
            Italic = false;
            Bold = false;
        }

        public TextComponent(string text)
        {
            Text = text;
            Colour = DefaultColour;
        }

        public TextComponent(string text, Colour colour)
        {
            Text = text;
            Colour = colour;
        }

        public TextComponent(string text, Colour colour, bool italic, bool bold)
        {
            Text = text;
            Colour = colour;
            Italic = italic;
            Bold = bold;
        }

        public void AddText(string text)
        {
            Text += text;
        }

        public static void SetDefaultColour(Colour colour)
        {
            DefaultColour = colour;
        }
    }
}

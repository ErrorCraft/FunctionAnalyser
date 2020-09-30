using System;

namespace AdvancedText
{
    public class TextComponent
    {
        public string Text { get; set; }
        public Colour Colour { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }

        public TextComponent(string text)
        {
            this.Text = text;
            this.Colour = Colour.BuiltinColours.BLACK;
        }

        public TextComponent(string text, string colour)
        {
            this.Text = text;
            this.Colour = colour;
        }

        public TextComponent(string text, string colour, bool italic, bool bold)
        {
            Text = text;
            Colour = colour;
            Italic = italic;
            Bold = bold;
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
    }
}

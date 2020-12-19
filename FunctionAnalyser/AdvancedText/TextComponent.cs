namespace AdvancedText
{
    public class TextComponent
    {
        private static Colour DefaultColour = Colour.BuiltinColours.BLACK;

        public static void SetDefaultColour(Colour colour)
        {
            DefaultColour = colour;
        }

        public string Text { get; private set; }
        public Colour Colour { get; private set; }
        public bool Italic { get; private set; }
        public bool Bold { get; private set; }
        public TextComponent Child { get; private set; }

        public TextComponent(string text)
        {
            Text = text;
            Colour = DefaultColour;

            Italic = false;
            Bold = false;
        }

        public TextComponent WithColour(Colour colour)
        {
            Colour = colour;
            return this;
        }

        public TextComponent WithStyle(bool italic, bool bold)
        {
            Italic = italic;
            Bold = bold;
            return this;
        }

        public TextComponent With(TextComponent child)
        {
            if (Child == null)
            {
                Child = child;
            } else
            {
                TextComponent lastChild = Child;
                while (lastChild.Child != null)
                {
                    lastChild = lastChild.Child;
                }
                lastChild.With(child);
            }
            return this;
        }

        public void AppendText(string text)
        {
            Text += text;
        }
    }
}

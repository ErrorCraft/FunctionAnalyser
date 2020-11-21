using System.Collections.Generic;

namespace CommandParser.Results.Arguments
{
    public class Message
    {
        public string Text { get; }
        public Dictionary<int, EntitySelector> Selectors { get; }

        public Message(string text, Dictionary<int, EntitySelector> selectors)
        {
            Text = text;
            Selectors = selectors;
        }
    }
}

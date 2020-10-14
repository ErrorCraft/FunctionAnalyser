using System.Collections.Generic;

namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class Object : IComponent
    {
        public Dictionary<string, IComponent> Values { get; private set; }
        public Object()
        {
            Values = new Dictionary<string, IComponent>();
        }

        public override string ToString()
        {
            string s = "{";
            foreach (string key in Values.Keys)
            {
                s += "\"" + key + "\":" + Values[key].ToString() + ",";
            }
            return s.TrimEnd(',') + "}";
        }
        public string AsJson()
        {
            string s = "{";
            foreach (string key in Values.Keys)
            {
                s += "\"" + key + "\":" + Values[key].AsJson() + ",";
            }
            return s.TrimEnd(',') + "}";
        }
        public string GetName() => Name;
        public static string Name { get { return "Object"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            if (!ValidateContents(reader, start, mayThrow)) return false;
            if (!ValidateChildren(reader, start, mayThrow)) return false;
            if (!ValidateFormatting(reader, start, mayThrow)) return false;
            if (!ValidateInteractivity(reader, start, mayThrow)) return false;
            return true;
        }

        private bool ValidateContents(StringReader reader, int start, bool mayThrow)
        {
            foreach (string contentKey in Components.Content.Keys)
            {
                if (Values.ContainsKey(contentKey))
                {
                    return Components.Content[contentKey].Validate(this, contentKey, reader, start, mayThrow);
                }
            }
            reader.SetCursor(start);
            if (mayThrow) ComponentErrors.UnknownComponentError(this).AddWithContext(reader);
            return false;
        }

        private bool ValidateChildren(StringReader reader, int start, bool mayThrow)
        {
            foreach (string childrenKey in Components.Children.Keys)
            {
                if (Values.ContainsKey(childrenKey))
                {
                    return Components.Children[childrenKey].Validate(this, childrenKey, reader, start, mayThrow);
                }
            }
            return true;
        }

        private bool ValidateFormatting(StringReader reader, int start, bool mayThrow)
        {
            foreach (string formattingKey in Components.Formatting.Keys)
            {
                if (Values.ContainsKey(formattingKey))
                {
                    return Components.Formatting[formattingKey].Validate(this, formattingKey, reader, start, mayThrow);
                }
            }
            return true;
        }

        private bool ValidateInteractivity(StringReader reader, int start, bool mayThrow)
        {
            foreach (string interactivityKey in Components.Interactivity.Keys)
            {
                if (Values.ContainsKey(interactivityKey))
                {
                    return Components.Interactivity[interactivityKey].Validate(this, interactivityKey, reader, start, mayThrow);
                }
            }
            return true;
        }
    }
}

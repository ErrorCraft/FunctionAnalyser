using System.Collections.Generic;

namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class Array : IComponent
    {
        public List<IComponent> Values { get; private set; }
        public Array()
        {
            Values = new List<IComponent>();
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].ToString() + ",";
            }
            return s.TrimEnd(',') + "]";
        }

        public string AsJson()
        {
            string s = "[";
            for (int i = 0; i < Values.Count; i++)
            {
                s += Values[i].AsJson() + ",";
            }
            return s.TrimEnd(',') + "]";
        }
        public string GetName() => Name;
        public static string Name { get { return "Array"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            if (Values.Count == 0)
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.EmptyComponentError().AddWithContext(reader);
                return false;
            }
            for (int i = 0; i < Values.Count; i++)
            {
                if (!Values[i].Validate(reader, start, mayThrow)) return false;
            }
            return true;
        }
    }
}

using CommandVerifier.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.ComponentParser.Types
{
    class Array : IComponent
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
        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            if (Values.Count == 0)
            {
                if (mayThrow) CommandError.InvalidChatComponent("empty").AddWithContext(reader);
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

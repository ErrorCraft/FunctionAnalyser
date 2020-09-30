using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.ComponentParser.Types
{
    class String : IComponent
    {
        public string Value { get { return _value; } }
        private string _value;
        public String(string value)
        {
            _value = value;
        }

        public static implicit operator string(String d)
        {
            return d._value;
        }
        public static implicit operator String(string d)
        {
            return new String(d);
        }

        public override string ToString() => '"' + _value + '"';
        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

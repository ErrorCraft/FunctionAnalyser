using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CommandVerifier.ComponentParser.Types
{
    class Number : IComponent
    {
        private string _value;
        public Number(string value)
        {
            _value = value;
        }

        public static implicit operator string(Number d)
        {
            return d._value;
        }
        public static implicit operator Number(string d)
        {
            return new Number(d);
        }

        public override string ToString() => _value;
        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

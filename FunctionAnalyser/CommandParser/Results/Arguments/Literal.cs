using System;

namespace CommandParser.Results.Arguments
{
    public class Literal
    {
        public string Value { get; }

        public Literal(string literal)
        {
            Value = literal;
        }

        public override bool Equals(object obj)
        {
            return obj is Literal literal &&
                   Value == literal.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}

namespace CommandVerifier.ComponentParser.Types
{
    class Boolean : IComponent
    {
        private readonly bool _value;
        public Boolean(bool value)
        {
            _value = value;
        }

        public static implicit operator bool(Boolean d)
        {
            return d._value;
        }
        public static implicit operator Boolean(bool d)
        {
            return new Boolean(d);
        }

        public override string ToString() => _value ? "true" : "false";
        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

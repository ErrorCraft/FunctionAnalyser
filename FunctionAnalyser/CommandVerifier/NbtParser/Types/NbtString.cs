namespace CommandVerifier.NbtParser.Types
{
    class NbtString : INbtArgument
    {
        private readonly string _value;
        public NbtString(string value)
        {
            _value = value;
        }

        public string Get() => INbtArgument.TryQuote(_value);
        public string Id { get; } = "TAG_String";

        public static implicit operator string(NbtString d)
        {
            return d._value;
        }
        public static implicit operator NbtString(string d)
        {
            return new NbtString(d);
        }
    }
}

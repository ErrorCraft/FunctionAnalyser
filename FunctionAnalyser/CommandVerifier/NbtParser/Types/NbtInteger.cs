namespace CommandVerifier.NbtParser.Types
{
    class NbtInteger : NbtArgument
    {
        readonly int _value;
        public NbtInteger(int value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(NbtArgument.NbtNumberFormatInfo);
        public string Id { get; } = "TAG_Integer";

        public static implicit operator int(NbtInteger d)
        {
            return d._value;
        }
        public static implicit operator NbtInteger(int d)
        {
            return new NbtInteger(d);
        }
    }
}

namespace CommandVerifier.NbtParser.Types
{
    class NbtDouble : NbtArgument
    {
        readonly double _value;
        public NbtDouble(double value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(NbtArgument.NbtNumberFormatInfo) + 'd';
        public string Id { get; } = "TAG_Double";

        public static implicit operator double(NbtDouble d)
        {
            return d._value;
        }
        public static implicit operator NbtDouble(double d)
        {
            return new NbtDouble(d);
        }
    }
}

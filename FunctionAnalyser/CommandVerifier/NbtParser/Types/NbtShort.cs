namespace CommandVerifier.NbtParser.Types
{
    class NbtShort : NbtArgument
    {
        readonly short _value;
        public NbtShort(short value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(NbtArgument.NbtNumberFormatInfo) + 's';
        public string Id { get; } = "TAG_Short";

        public static implicit operator short(NbtShort d)
        {
            return d._value;
        }
        public static implicit operator NbtShort(short d)
        {
            return new NbtShort(d);
        }
    }
}

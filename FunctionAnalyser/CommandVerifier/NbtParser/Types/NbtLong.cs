namespace CommandVerifier.NbtParser.Types
{
    class NbtLong : INbtArgument
    {
        private readonly long _value;
        public NbtLong(long value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(INbtArgument.NbtNumberFormatInfo) + 'L';
        public string Id { get; } = "TAG_Long";

        public static implicit operator long(NbtLong d)
        {
            return d._value;
        }
        public static implicit operator NbtLong(long d)
        {
            return new NbtLong(d);
        }
    }
}

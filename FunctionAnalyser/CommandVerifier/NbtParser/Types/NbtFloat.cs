namespace CommandVerifier.NbtParser.Types
{
    class NbtFloat : INbtArgument
    {
        private readonly float _value;
        public NbtFloat(float value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(INbtArgument.NbtNumberFormatInfo) + 'f';
        public string Id { get; } = "TAG_Float";

        public static implicit operator float(NbtFloat d)
        {
            return d._value;
        }
        public static implicit operator NbtFloat(float d)
        {
            return new NbtFloat(d);
        }
    }
}

﻿namespace CommandVerifier.NbtParser.Types
{
    class NbtByte : NbtArgument
    {
        readonly sbyte _value;
        public NbtByte(sbyte value)
        {
            _value = value;
        }

        public string Get() => _value.ToString(NbtArgument.NbtNumberFormatInfo) + 'b';
        public string Id { get; } = "TAG_Byte";

        public static implicit operator sbyte(NbtByte d)
        {
            return d._value;
        }
        public static implicit operator NbtByte(sbyte d)
        {
            return new NbtByte(d);
        }
    }
}

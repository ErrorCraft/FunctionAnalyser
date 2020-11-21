namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtByte : NbtItem<sbyte>
    {
        public NbtByte(sbyte value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Byte";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}b";
        }
    }
}

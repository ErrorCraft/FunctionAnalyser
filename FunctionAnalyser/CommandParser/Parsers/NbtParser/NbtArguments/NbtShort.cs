namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtShort : NbtItem<short>
    {
        public NbtShort(short value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Short";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}s";
        }
    }
}

namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtInteger : NbtItem<int>
    {
        public NbtInteger(int value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Integer";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}";
        }
    }
}

namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtLong : NbtItem<long>
    {
        public NbtLong(long value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Long";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}L";
        }
    }
}

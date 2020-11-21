namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtDouble : NbtItem<double>
    {
        public NbtDouble(double value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Double";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}d";
        }
    }
}

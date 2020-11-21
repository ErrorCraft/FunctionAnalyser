namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtFloat : NbtItem<float>
    {
        public NbtFloat(float value) : base(value) { }

        public override string GetName()
        {
            return "TAG_Float";
        }

        public override string ToSnbt()
        {
            return $"{Value.ToString(NbtNumberProvider.NumberFormatInfo)}f";
        }
    }
}

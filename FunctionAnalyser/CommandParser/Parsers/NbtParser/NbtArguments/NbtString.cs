namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtString : NbtItem<string>
    {
        public NbtString(string value) : base(value) { }

        public override string GetName()
        {
            return "TAG_String";
        }

        public override string ToSnbt()
        {
            return $"'{Value}'"; // escape value
        }
    }
}

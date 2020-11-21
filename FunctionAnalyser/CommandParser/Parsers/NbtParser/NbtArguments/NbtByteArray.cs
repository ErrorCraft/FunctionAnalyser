namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtByteArray : NbtCollection<NbtByte>
    {
        public override string GetName()
        {
            return "TAG_Byte_Array";
        }

        public override string ToSnbt()
        {
            return $"[B; {ChildrenSnbt()}]";
        }

        protected override bool CanAdd(INbtArgument argument)
        {
            return argument is NbtByte;
        }
    }
}

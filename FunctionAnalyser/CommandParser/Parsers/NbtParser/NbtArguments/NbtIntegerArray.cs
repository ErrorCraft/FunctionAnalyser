namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtIntegerArray : NbtCollection<NbtInteger>
    {
        public override string GetName()
        {
            return "TAG_Integer_Array";
        }

        public override string ToSnbt()
        {
            return $"[I; {ChildrenSnbt()}]";
        }

        protected override bool CanAdd(INbtArgument argument)
        {
            return argument is NbtInteger;
        }
    }
}

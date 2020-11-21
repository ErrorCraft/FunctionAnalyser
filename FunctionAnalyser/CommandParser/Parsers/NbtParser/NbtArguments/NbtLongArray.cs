namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtLongArray : NbtCollection<NbtLong>
    {
        public override string GetName()
        {
            return "TAG_Long_Array";
        }

        public override string ToSnbt()
        {
            return $"[L; {ChildrenSnbt()}]";
        }

        protected override bool CanAdd(INbtArgument argument)
        {
            return argument is NbtLong;
        }
    }
}

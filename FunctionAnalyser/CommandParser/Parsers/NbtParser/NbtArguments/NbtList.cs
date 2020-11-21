namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public class NbtList<T> : NbtCollection<T> where T : INbtArgument
    {
        public NbtList(T firstValue) : base()
        {
            Values.Add(firstValue);
        }

        public NbtList() : base() { }

        public override string GetName()
        {
            if (Values.Count == 0 || (Values[0] is NbtList<T>)) return "TAG_List";
            else return $"list of {Values[0].GetName()}";
        }

        public override string ToSnbt()
        {
            return $"[{ChildrenSnbt()}]";
        }

        protected override bool CanAdd(INbtArgument argument)
        {
            if (Values.Count == 0) return true;
            else return argument.GetType() == Values[0].GetType();
        }
    }
}

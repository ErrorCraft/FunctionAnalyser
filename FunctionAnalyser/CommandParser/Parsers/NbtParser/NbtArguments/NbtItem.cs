namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public abstract class NbtItem<T> : INbtArgument
    {
        private protected readonly T Value;

        public NbtItem(T value)
        {
            Value = value;
        }

        public abstract string GetName();
        public abstract string ToSnbt();

        public static implicit operator T(NbtItem<T> s)
        {
            return s.Value;
        }
    }
}

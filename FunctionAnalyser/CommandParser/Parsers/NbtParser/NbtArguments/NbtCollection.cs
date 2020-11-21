using System.Collections.Generic;

namespace CommandParser.Parsers.NbtParser.NbtArguments
{
    public interface INbtCollection : INbtArgument
    {
        bool TryAdd(INbtArgument argument);
    }

    public abstract class NbtCollection<T> : INbtCollection where T : INbtArgument
    {
        private protected readonly List<T> Values;

        public NbtCollection()
        {
            Values = new List<T>();
        }

        public abstract string ToSnbt();
        private protected string ChildrenSnbt()
        {
            string snbt = "";
            for (int i = 0; i < Values.Count; i++)
            {
                snbt += Values[i].ToSnbt() + ", ";
            }
            return snbt.TrimEnd(',', ' ');
        }

        protected abstract bool CanAdd(INbtArgument argument);

        public bool TryAdd(INbtArgument argument)
        {
            if (argument is T actualArgument && CanAdd(actualArgument))
            {
                Values.Add(actualArgument);
                return true;
            }
            return false;
        }

        public abstract string GetName();
    }
}

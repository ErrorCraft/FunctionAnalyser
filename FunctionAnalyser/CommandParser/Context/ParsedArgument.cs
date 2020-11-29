namespace CommandParser.Context
{
    public abstract class ParsedArgument
    {
        protected ParsedArgument() { }
        public abstract object GetResult();
        public abstract bool IsFromRoot();
    }

    public class ParsedArgument<T> : ParsedArgument
    {
        private readonly T Result;
        private readonly bool FromRoot;

        public ParsedArgument(T result, bool fromRoot)
        {
            Result = result;
            FromRoot = fromRoot;
        }

        public override object GetResult()
        {
            return Result;
        }

        public override bool IsFromRoot()
        {
            return FromRoot;
        }
    }
}

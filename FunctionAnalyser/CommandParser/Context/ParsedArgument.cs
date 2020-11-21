namespace CommandParser.Context
{
    public abstract class ParsedArgument
    {
        protected ParsedArgument() { }
        public abstract object GetResult();
    }

    public class ParsedArgument<T> : ParsedArgument
    {
        private readonly T Result;

        public ParsedArgument(T result)
        {
            Result = result;
        }

        public override object GetResult()
        {
            return Result;
        }
    }
}

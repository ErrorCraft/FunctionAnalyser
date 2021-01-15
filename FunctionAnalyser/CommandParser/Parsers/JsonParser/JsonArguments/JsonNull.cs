namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonNull : IJsonArgument
    {
        public const string NAME = "Null";
        public const string NULL = "null";

        public string AsJson()
        {
            return NULL;
        }

        public string GetName() => NAME;

        public override string ToString()
        {
            return NULL;
        }
    }
}

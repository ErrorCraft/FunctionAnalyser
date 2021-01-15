namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonBoolean : IJsonArgument
    {
        public const string NAME = "Boolean";
        public const string TRUE = "true";
        public const string FALSE = "false";
        
        private readonly bool Boolean;

        public JsonBoolean(bool boolean)
        {
            Boolean = boolean;
        }

        public string AsJson()
        {
            return Boolean ? TRUE : FALSE;
        }

        public string GetName() => NAME;

        public override string ToString()
        {
            return Boolean ? TRUE : FALSE;
        }
    }
}

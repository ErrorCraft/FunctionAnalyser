namespace CommandParser.Parsers.JsonParser.JsonArguments
{
    public class JsonNumber : IJsonArgument
    {
        public const string NAME = "Number";

        private readonly string Number;

        public JsonNumber(string number)
        {
            Number = number;
        }

        public string AsJson()
        {
            return Number;
        }

        public string GetName() => NAME;

        public JsonArgumentType GetArgumentType()
        {
            return JsonArgumentType.Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}

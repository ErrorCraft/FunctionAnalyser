using CommandParser.Results;

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

        public ReadResults ValidateComponent(IStringReader reader, int start)
        {
            return new ReadResults(true, null);
        }

        public override string ToString()
        {
            return Number;
        }
    }
}

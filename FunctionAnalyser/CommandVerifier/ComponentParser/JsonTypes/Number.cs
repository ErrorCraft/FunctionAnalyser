namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class Number : IComponent
    {
        private readonly string Value;
        public Number(string value)
        {
            Value = value;
        }
        public static implicit operator string(Number d)
        {
            return d.Value;
        }
        public static implicit operator Number(string d)
        {
            return new Number(d);
        }

        public override string ToString() => Value;
        public string AsJson() => Value;
        public string GetName() => Name;
        public static string Name { get { return "Number"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

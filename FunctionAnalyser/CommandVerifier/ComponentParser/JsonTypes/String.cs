namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class String : IComponent
    {
        private readonly string Value;
        public String(string value)
        {
            Value = value;
        }
        public static implicit operator string(String d)
        {
            return d.Value;
        }
        public static implicit operator String(string d)
        {
            return new String(d);
        }

        public override string ToString() => Value;
        public string AsJson() => '"' + Value + '"';
        public string GetName() => Name;
        public static string Name { get { return "String"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

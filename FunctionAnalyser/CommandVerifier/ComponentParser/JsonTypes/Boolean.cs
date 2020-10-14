namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class Boolean : IComponent
    {
        private readonly bool Value;
        public Boolean(bool value)
        {
            Value = value;
        }
        public static implicit operator bool(Boolean d)
        {
            return d.Value;
        }
        public static implicit operator Boolean(bool d)
        {
            return new Boolean(d);
        }

        public override string ToString() => Value ? "true" : "false";
        public string AsJson() => Value ? "true" : "false";
        public string GetName() => Name;
        public static string Name { get { return "Boolean"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow) => true;
    }
}

namespace CommandVerifier.ComponentParser.JsonTypes
{
    public class Null : IComponent
    {
        public override string ToString() => "null";
        public string AsJson() => "null";
        public string GetName() => Name;
        public static string Name { get { return "Null"; } }

        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            reader.SetCursor(start);
            if (mayThrow) ComponentError.UnknownComponentError(this).AddWithContext(reader);
            return false;
        }
    }
}

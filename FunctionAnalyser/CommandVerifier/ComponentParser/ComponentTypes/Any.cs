namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class Any : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            return obj.Values[key].Validate(reader, start, mayThrow);
        }
    }
}

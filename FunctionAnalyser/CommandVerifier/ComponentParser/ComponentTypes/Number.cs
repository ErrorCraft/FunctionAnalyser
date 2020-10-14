namespace CommandVerifier.ComponentParser.ComponentTypes
{
    public class Number : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!(obj.Values[key] is JsonTypes.Number))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.StringFormatError(key, JsonTypes.Number.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }
            return true;
        }
    }
}

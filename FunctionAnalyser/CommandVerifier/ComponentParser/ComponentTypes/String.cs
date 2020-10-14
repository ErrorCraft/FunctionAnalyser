namespace CommandVerifier.ComponentParser.ComponentTypes
{
    public class String : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!IsText(obj.Values[key]))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.StringFormatError(key, JsonTypes.String.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }

            return ValidateContents(obj, key, reader, start, mayThrow);
        }
    }
}

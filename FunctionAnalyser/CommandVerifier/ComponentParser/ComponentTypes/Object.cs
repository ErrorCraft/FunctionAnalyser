namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class Object : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!(obj.Values[key] is JsonTypes.Object actualObj))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentErrors.StringFormatError(key, JsonTypes.Object.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }

            return ValidateContents(actualObj, key, reader, start, mayThrow);
        }
    }
}

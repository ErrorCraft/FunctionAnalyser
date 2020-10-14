using CommandVerifier.Commands;

namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class Uuid : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!IsText(obj.Values[key]))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.StringFormatError(key, JsonTypes.String.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }

            if (!Types.Uuid.TryParse(obj.Values[key].ToString()))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidUuid().AddWithContext(reader);
                return false;
            }
            return true;
        }
    }
}

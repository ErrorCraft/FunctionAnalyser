using Newtonsoft.Json;

namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class Array : Component
    {
        public Array()
        {
            MayBeEmpty = false;
        }

        [JsonProperty("may_be_empty")]
        private readonly bool MayBeEmpty;

        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!(obj.Values[key] is JsonTypes.Array actualObj))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentErrors.StringFormatError(key, JsonTypes.Array.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }

            if (actualObj.Values.Count == 0 && !MayBeEmpty)
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentErrors.EmptyComponentError().AddWithContext(reader); // Unexpected empty array?
                return false;
            }

            for (int i = 0; i < actualObj.Values.Count; i++)
            {
                if (!actualObj.Values[i].Validate(reader, start, mayThrow)) return false;
            }

            return true;
        }

        public static string Name { get { return "Array"; } }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class Binding : Component
    {
        public Binding()
        {
            BindTo = "";
            Values = new Dictionary<string, Component>();
        }

        [JsonProperty("bind_to")]
        private readonly string BindTo;

        [JsonProperty("values")]
        private readonly Dictionary<string, Component> Values;

        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!obj.Values.ContainsKey(BindTo)) return true;
            if (!IsText(obj.Values[BindTo]))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.StringFormatError(key, JsonTypes.String.Name, obj.Values[BindTo].GetName()).AddWithContext(reader);
                return false;
            }
            string binding = obj.Values[BindTo].ToString();
            List<string> keys = new List<string>(Values.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                if (binding.Equals(keys[i]))
                {
                    return Values[binding].Validate(obj, key, reader, start, mayThrow);
                }
            }
            return true;
        }
    }
}

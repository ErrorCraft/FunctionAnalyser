using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ProgramUpdater.Converters
{
    public class TagNameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string tagName = JToken.Load(reader).ToObject<string>().Replace("v", null);
            return System.Version.TryParse(tagName, out System.Version actualTagName) ? actualTagName : null;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

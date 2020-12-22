using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CommandFilesApi.Converters
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
            return Version.TryParse(tagName, out Version actualTagName) ? actualTagName : null;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

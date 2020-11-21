using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CommandParser.Converters
{
    public abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(JObject jObject, JsonSerializer serializer);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            return Create(jObject, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

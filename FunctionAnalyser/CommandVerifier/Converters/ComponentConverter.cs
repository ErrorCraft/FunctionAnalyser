using CommandVerifier.ComponentParser.ComponentTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using ComponentTypes = CommandVerifier.ComponentParser.ComponentTypes;

namespace CommandVerifier.Converters
{
    class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Component).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null;
            return base.ResolveContractConverter(objectType);
        }
    }

    class ComponentConverter : JsonConverter
    {
        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Component);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["type"].Value<string>())
            {
                case "string":
                    return JsonConvert.DeserializeObject<ComponentTypes.String>(jo.ToString(), SpecifiedSubclassConversion);
                case "object":
                    return JsonConvert.DeserializeObject<ComponentTypes.Object>(jo.ToString(), SpecifiedSubclassConversion);
                case "array":
                    return JsonConvert.DeserializeObject<ComponentTypes.Array>(jo.ToString(), SpecifiedSubclassConversion);
                case "binding":
                    return JsonConvert.DeserializeObject<Binding>(jo.ToString(), SpecifiedSubclassConversion);
                case "any":
                    return JsonConvert.DeserializeObject<Any>(jo.ToString(), SpecifiedSubclassConversion);
                case "number":
                    return JsonConvert.DeserializeObject<Number>(jo.ToString(), SpecifiedSubclassConversion);
                case "namespaced_id":
                    return JsonConvert.DeserializeObject<NamespacedId>(jo.ToString(), SpecifiedSubclassConversion);
                case "uuid":
                    return JsonConvert.DeserializeObject<Uuid>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception("Type '" + jo["type"].Value<string>() + "' is not a valid component type");
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

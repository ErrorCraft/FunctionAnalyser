using CommandParser.Parsers.ComponentParser.ComponentArguments;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CommandParser.Converters
{
    public class ComponentConverter : AbstractJsonConverter<ComponentArgument>
    {
        protected override ComponentArgument Create(JObject jObject, JsonSerializer serializer)
        {
            string type = jObject["type"].Value<string>();

            ComponentArgument componentArgument = type switch
            {
                "string" => new ComponentString(),
                "object" => new ComponentObject(),
                "array" => new ComponentArray(),
                "binding" => new ComponentBinding(),
                "any" => new ComponentAny(),
                "number" => new ComponentNumber(),
                "resource_location" => new ComponentResourceLocation(),
                "uuid" => new ComponentUuid(),
                "root" => new ComponentRoot(),
                "array_or_root" => new ComponentArrayOrRoot(),
                _ => throw new ArgumentException($"'{type}' is not a valid component type")
            };

            serializer.Populate(jObject.CreateReader(), componentArgument);
            return componentArgument;
        }
    }
}

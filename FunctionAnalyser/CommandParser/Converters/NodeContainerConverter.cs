using CommandParser.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CommandParser.Converters
{
    public class NodeContainerConverter : AbstractJsonConverter<Dictionary<string, Node>>
    {
        protected override Dictionary<string, Node> Create(JObject jObject, JsonSerializer serializer)
        {
            Dictionary<string, Node> result = new Dictionary<string, Node>();
            foreach (JProperty property in jObject.Properties())
            {
                result.Add(property.Name, GetNode(property.Value, property.Name, serializer));
            }
            return result;
        }

        private static Node GetNode(JToken jToken, string name, JsonSerializer serializer)
        {
            string type = jToken["type"].Value<string>();

            return type switch
            {
                "literal" => NodeConverter.GetLiteral(jToken, name, serializer),
                "argument" => NodeConverter.GetArgument(jToken, name, serializer),
                _ => throw new ArgumentException($"'{type}' is not a valid node type")
            };
        }
    }
}

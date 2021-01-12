using CommandParser.Parsers.ComponentParser.ComponentArguments;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using System.Collections.Generic;

namespace CommandParser.Parsers.ComponentParser
{
    public class ComponentReader
    {
        private readonly JsonObject Json;
        private readonly IStringReader StringReader;
        private readonly int Start;
        private readonly DispatcherResources Resources;

        public ComponentReader(JsonObject json, IStringReader stringReader, int start, DispatcherResources resources)
        {
            Json = json;
            StringReader = stringReader;
            Start = start;
            Resources = resources;
        }

        public ReadResults Validate()
        {
            ReadResults readResults = ValidatePrimary();
            if (readResults.Successful) readResults = ValidateOptional();
            return readResults;
        }

        private ReadResults ValidatePrimary()
        {
            Dictionary<string, ComponentArgument> components = Resources.Components.GetPrimary();
            foreach (string key in components.Keys)
            {
                if (Json.ContainsKey(key))
                {
                    return components[key].Validate(Json, key, StringReader, Start, Resources);
                }
            }
            StringReader.SetCursor(Start);
            return new ReadResults(false, ComponentCommandError.UnknownComponentError(Json).WithContext(StringReader));
        }

        private ReadResults ValidateOptional()
        {
            ReadResults readResults;
            Dictionary<string, ComponentArgument> components = Resources.Components.GetOptional();
            foreach (string key in components.Keys)
            {
                if (Json.ContainsKey(key))
                {
                    readResults = components[key].Validate(Json, key, StringReader, Start, Resources);
                    if (!readResults.Successful) return readResults;
                }
            }
            return new ReadResults(true, null);
        }
    }
}

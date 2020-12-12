using CommandParser.Collections;
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

        public ComponentReader(JsonObject json, IStringReader stringReader, int start)
        {
            Json = json;
            StringReader = stringReader;
            Start = start;
        }

        public ReadResults Validate()
        {
            ReadResults readResults = ValidateContents();
            if (readResults.Successful) readResults = ValidateChildren();
            if (readResults.Successful) readResults = ValidateFormatting();
            if (readResults.Successful) readResults = ValidateInteractivity();
            return readResults;
        }

        private ReadResults ValidateContents()
        {
            Dictionary<string, ComponentArgument> components = Components.GetContent();
            foreach (string key in components.Keys)
            {
                if (Json.ContainsKey(key))
                {
                    return components[key].Validate(Json, key, StringReader, Start);
                }
            }
            StringReader.SetCursor(Start);
            return new ReadResults(false, ComponentCommandError.UnknownComponentError(Json).WithContext(StringReader));
        }

        private ReadResults ValidateChildren()
        {
            return ValidateOptionalComponents(Components.GetChildren());
        }

        private ReadResults ValidateFormatting()
        {
            return ValidateOptionalComponents(Components.GetFormatting());
        }

        private ReadResults ValidateInteractivity()
        {
            return ValidateOptionalComponents(Components.GetInteractivity());
        }

        private ReadResults ValidateOptionalComponents(Dictionary<string, ComponentArgument> components)
        {
            ReadResults readResults;
            foreach (string key in components.Keys)
            {
                if (Json.ContainsKey(key))
                {
                    readResults = components[key].Validate(Json, key, StringReader, Start);
                    if (!readResults.Successful) return readResults;
                }
            }
            return new ReadResults(true, null);
        }
    }
}

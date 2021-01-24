using CommandParser.Collections;
using CommandParser.Parsers.ComponentParser.ComponentArguments;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using System.Collections.Generic;

namespace CommandParser.Parsers.ComponentParser
{
    public class ComponentReader
    {
        private readonly IStringReader StringReader;
        private readonly int Start;
        private readonly DispatcherResources Resources;

        public ComponentReader(IStringReader stringReader, int start, DispatcherResources resources)
        {
            StringReader = stringReader;
            Start = start;
            Resources = resources;
        }

        public ReadResults ValidateFromRoot(IJsonArgument json, Components components)
        {
            ComponentArgument root = components.GetRootComponent();
            JsonObject rootObject = new JsonObject();
            rootObject.Add("root", json);
            return root.Validate(rootObject, "root", this, components, StringReader, Start, Resources);
        }

        public ReadResults ValidateContents(IJsonArgument json, Components components)
        {
            if (json is JsonNull)
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, ComponentCommandError.UnknownComponent(json).WithContext(StringReader));
            }
            else if (json is JsonObject jsonObject) return ValidateObject(jsonObject, components);
            else if (json is JsonArray jsonArray) return ValidateArray(jsonArray, components);
            else return new ReadResults(true, null);
        }

        public ReadResults ValidateObject(JsonObject json, Components components)
        {
            ReadResults readResults = ValidatePrimary(json, components);
            if (readResults.Successful) readResults = ValidateOptional(json, components);
            return readResults;
        }

        private ReadResults ValidateArray(JsonArray json, Components components)
        {
            if (json.GetLength() == 0)
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, ComponentCommandError.EmptyComponent().WithContext(StringReader));
            }
            ReadResults readResults;
            for (int i = 0; i < json.GetLength(); i++)
            {
                readResults = ValidateContents(json[i], components);
                if (!readResults.Successful) return readResults;
            }
            return new ReadResults(true, null);
        }

        private ReadResults ValidatePrimary(JsonObject json, Components components)
        {
            Dictionary<string, ComponentArgument> componentArguments = components.GetPrimary();
            if (componentArguments == null || componentArguments.Count == 0) return new ReadResults(true, null);
            foreach (string key in componentArguments.Keys)
            {
                if (json.TryGetKey(key, componentArguments[key].MayUseKeyResourceLocation(), out string actualKey))
                {
                    return componentArguments[key].Validate(json, actualKey, this, components, StringReader, Start, Resources);
                }
            }
            StringReader.SetCursor(Start);
            return new ReadResults(false, ComponentCommandError.UnknownComponent(json).WithContext(StringReader));
        }

        private ReadResults ValidateOptional(JsonObject json, Components components)
        {
            Dictionary<string, ComponentArgument> componentArguments = components.GetOptional();
            if (componentArguments == null || componentArguments.Count == 0) return new ReadResults(true, null);
            ReadResults readResults;
            foreach (string key in componentArguments.Keys)
            {
                if (json.TryGetKey(key, componentArguments[key].MayUseKeyResourceLocation(), out string actualKey))
                {
                    readResults = componentArguments[key].Validate(json, actualKey, this, components, StringReader, Start, Resources);
                    if (!readResults.Successful) return readResults;
                }
            }
            return new ReadResults(true, null);
        }
    }
}

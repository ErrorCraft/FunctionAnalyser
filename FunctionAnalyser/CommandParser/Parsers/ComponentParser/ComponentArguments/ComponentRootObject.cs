﻿using CommandParser.Collections;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;

namespace CommandParser.Parsers.ComponentParser.ComponentArguments
{
    public class ComponentRootObject : ComponentArgument
    {
        public override ReadResults Validate(JsonObject obj, string key, ComponentReader componentReader, Components components, IStringReader reader, int start, DispatcherResources resources)
        {
            if (obj.GetChild(key) is not JsonObject actualObject)
            {
                reader.SetCursor(start);
                return ReadResults.Failure(ComponentCommandError.InvalidComponent(key, JsonArgumentType.Object, obj.GetChild(key).GetArgumentType()).WithContext(reader));
            }
            return componentReader.ValidateObject(actualObject, components);
        }
    }
}

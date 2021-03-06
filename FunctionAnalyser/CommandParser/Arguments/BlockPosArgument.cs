﻿using CommandParser.Minecraft.Coordinates;
using CommandParser.Parsers.Coordinates;
using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments {
    public class BlockPosArgument : IArgument<ICoordinates> {
        [JsonProperty("use_bedrock")]
        private readonly bool UseBedrock;

        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ICoordinates result) {
            if (reader.CanRead() && reader.Peek() == '^') {
                return new LocalCoordinatesParser(reader, UseBedrock).Parse(out result);
            }
            return new WorldCoordinatesParser(reader, UseBedrock).ParseInteger(out result);
        }
    }
}

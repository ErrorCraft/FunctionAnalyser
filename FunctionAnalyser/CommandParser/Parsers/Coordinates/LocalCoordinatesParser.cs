﻿using CommandParser.Minecraft.Coordinates;
using CommandParser.Results;

namespace CommandParser.Parsers.Coordinates {
    public class LocalCoordinatesParser {
        private readonly IStringReader StringReader;
        private readonly int Start;
        private readonly bool UseBedrock;

        public LocalCoordinatesParser(IStringReader stringReader, bool useBedrock) {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(out ICoordinates result) {
            result = default;

            ReadResults readResults = ReadDouble(out double left);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else {
                if (!StringReader.AtEndOfArgument()) {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = ReadDouble(out double up);
            if (!readResults.Successful) return readResults;
            if (UseBedrock) StringReader.SkipWhitespace();
            else {
                if (!StringReader.AtEndOfArgument()) {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.Vec3CoordinatesIncomplete().WithContext(StringReader));
                }
                StringReader.Skip();
            }

            readResults = ReadDouble(out double forwards);
            if (readResults.Successful) result = new LocalCoordinates(left, up, forwards);
            return readResults;
        }

        private ReadResults ReadDouble(out double result) {
            result = default;
            if (!StringReader.CanRead()) {
                return ReadResults.Failure(CommandError.ExpectedCoordinate().WithContext(StringReader));
            }
            if (StringReader.Peek() != '^') {
                return ReadResults.Failure(CommandError.MixedCoordinateType().WithContext(StringReader));
            }
            StringReader.Skip();
            if (StringReader.AtEndOfArgument() || (UseBedrock && !IsNumberPart(StringReader.Peek()))) {
                result = 0.0d;
                return ReadResults.Success();
            } else {
                return StringReader.ReadDouble(out result);
            }
        }

        private static bool IsNumberPart(char c) {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }
    }
}

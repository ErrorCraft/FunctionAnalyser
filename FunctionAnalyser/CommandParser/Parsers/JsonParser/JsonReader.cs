using CommandParser.Parsers.JsonParser.JsonArguments;
using static CommandParser.Parsers.JsonParser.JsonCharacterProvider;
using CommandParser.Results;
using System.Globalization;

namespace CommandParser.Parsers.JsonParser
{
    public class JsonReader
    {
        private readonly IStringReader StringReader;
        private readonly int Start;

        public JsonReader(IStringReader stringReader)
        {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
        }

        public ReadResults ReadAny(out IJsonArgument result)
        {
            result = new JsonNull();

            SkipWhitespace();
            if (!StringReader.CanRead()) return new ReadResults(true, null);

            ReadResults readResults;
            switch (StringReader.Peek())
            {
                case OBJECT_OPEN_CHARACTER:
                    readResults = ReadObject(out JsonObject objectResult);
                    if (readResults.Successful) result = objectResult;
                    return readResults;
                case ARRAY_OPEN_CHARACTER:
                    readResults = ReadArray(out JsonArray arrayResult);
                    if (readResults.Successful) result = arrayResult;
                    return readResults;
                case STRING_CHARACTER:
                    readResults = ReadString(out JsonString stringResult);
                    if (readResults.Successful) result = stringResult;
                    return readResults;
                default:
                    return ReadSpecial(out result);
            }
        }

        public ReadResults ReadObject(out JsonObject result)
        {
            result = new JsonObject();

            if (!StringReader.CanRead() || StringReader.Peek() != OBJECT_OPEN_CHARACTER) return new ReadResults(false, JsonCommandError.MalformedJson());
            StringReader.Skip();

            SkipWhitespace();
            ReadResults readResults;
            while (StringReader.CanRead() && StringReader.Peek() != OBJECT_CLOSE_CHARACTER)
            {
                SkipWhitespace();
                readResults = ReadString(out JsonString name);
                if (!readResults.Successful) return readResults;

                SkipWhitespace();
                if (!StringReader.CanRead() || StringReader.Peek() != NAME_VALUE_SEPARATOR) return new ReadResults(false, JsonCommandError.ExpectedNameValueSeparator().WithContext(StringReader));
                StringReader.Skip();

                SkipWhitespace();
                readResults = ReadAny(out IJsonArgument argument);
                if (!readResults.Successful) return readResults;
                result.Add(name, argument);

                SkipWhitespace();
                if (!StringReader.CanRead() || StringReader.Peek() != ARGUMENT_SEPARATOR) break;
                StringReader.Skip();
            }
            if (!StringReader.CanRead() || StringReader.Peek() != OBJECT_CLOSE_CHARACTER) return new ReadResults(false, JsonCommandError.EndOfInput().WithContext(StringReader));
            StringReader.Skip();
            return new ReadResults(true, null);
        }

        public ReadResults ReadArray(out JsonArray result)
        {
            result = new JsonArray();

            if (!StringReader.CanRead() || StringReader.Peek() != ARRAY_OPEN_CHARACTER) return new ReadResults(false, JsonCommandError.MalformedJson());
            StringReader.Skip();

            SkipWhitespace();
            ReadResults readResults;
            while (StringReader.CanRead() && StringReader.Peek() != ARRAY_CLOSE_CHARACTER)
            {
                SkipWhitespace();
                readResults = ReadAny(out IJsonArgument argument);
                if (!readResults.Successful) return readResults;
                result.Add(argument);

                SkipWhitespace();
                if (!StringReader.CanRead() || StringReader.Peek() != ARGUMENT_SEPARATOR) break;
                StringReader.Skip();
            }
            if (!StringReader.CanRead() || StringReader.Peek() != ARRAY_CLOSE_CHARACTER) return new ReadResults(false, JsonCommandError.EndOfInput().WithContext(StringReader));
            StringReader.Skip();
            return new ReadResults(true, null);
        }

        public ReadResults ReadString(out JsonString result)
        {
            result = default;
            string actualString = "";

            if (!StringReader.CanRead() || StringReader.Peek() != STRING_CHARACTER) return new ReadResults(false, JsonCommandError.MalformedJson().WithContext(StringReader));
            StringReader.Skip();

            bool escaped = false;
            while (StringReader.CanRead())
            {
                char c = StringReader.Read();
                if (escaped)
                {
                    if (c == UNICODE_CHARACTER)
                    {
                        if (!StringReader.CanRead(4)) return new ReadResults(false, JsonCommandError.UnterminatedEscapeSequence().WithContext(StringReader));
                        string unicode = StringReader.Read(4);
                        if (!short.TryParse(unicode, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out _)) return new ReadResults(false, JsonCommandError.InvalidUnicodeCharacter(unicode).WithContext(StringReader));
                        escaped = false;
                        actualString += c + unicode;
                    } else if (ESCAPABLE_CHARACTERS.Contains(c))
                    {
                        escaped = false;
                        actualString += c;
                    } else
                    {
                        return new ReadResults(false, JsonCommandError.InvalidEscapeSequence().WithContext(StringReader));
                    }
                } else if (c == ESCAPE_CHARACTER)
                {
                    escaped = true;
                    actualString += c;
                } else if (c == STRING_CHARACTER)
                {
                    result = new JsonString(actualString);
                    return new ReadResults(true, null);
                } else
                {
                    actualString += c;
                }
            }

            return new ReadResults(false, JsonCommandError.UnterminatedString().WithContext(StringReader));
        }

        public ReadResults ReadSpecial(out IJsonArgument result)
        {
            int start = StringReader.GetCursor();
            while (StringReader.CanRead() && IsAllowedInput(StringReader.Peek())) StringReader.Skip();
            string s = StringReader.GetString()[start..StringReader.GetCursor()];

            result = s switch
            {
                JsonBoolean.TRUE => new JsonBoolean(true),
                JsonBoolean.FALSE => new JsonBoolean(false),
                JsonNull.NULL => new JsonNull(),
                _ => default
            };
            if (double.TryParse(s, out _)) result = new JsonNumber(s);
            return result is null ? new ReadResults(false, JsonCommandError.MalformedJson().WithContext(StringReader)) :
                                    new ReadResults(true, null);
        }

        private void SkipWhitespace()
        {
            while (StringReader.CanRead() && IsWhitespace(StringReader.Peek())) StringReader.Skip();
        }

        private static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t';
        }

        private static bool IsAllowedInput(char c)
        {
            return !IsWhitespace(c) &&
                   c != NAME_VALUE_SEPARATOR && c != ARGUMENT_SEPARATOR &&
                   c != OBJECT_OPEN_CHARACTER && c != OBJECT_CLOSE_CHARACTER &&
                   c != ARRAY_OPEN_CHARACTER && c != ARRAY_CLOSE_CHARACTER &&
                   c != STRING_CHARACTER; // include other characters?
        }
    }
}

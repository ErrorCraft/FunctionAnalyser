using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using System.Globalization;
using System.Text;

namespace ErrorCraft.Minecraft.Json;

public class JsonReader {
    private readonly IStringReader Reader;

    public JsonReader(IStringReader reader) {
        Reader = reader;
    }

    public Result<IJsonElement> Read() {
        SkipWhitespace();
        if (Reader.IsNext(JsonString.QUOTE_CHARACTER)) {
            return ReadString();
        }
        return ReadElement();
    }

    private Result<IJsonElement> ReadString() {
        if (!Reader.IsNext(JsonString.QUOTE_CHARACTER)) {
            return Result<IJsonElement>.Failure(new Message("Malformed JSON"));
        }
        Reader.Skip();

        Result<string> stringResult = ReadCharacters();
        return Result<IJsonElement>.From(stringResult, (value) => new JsonString(value));
    }

    private Result<string> ReadCharacters() {
        StringBuilder stringBuilder = new StringBuilder();
        while (Reader.CanRead()) {
            char c = Reader.Read();
            if (c == JsonString.QUOTE_CHARACTER) {
                return Result<string>.Success(stringBuilder.ToString());
            }
            if (c == JsonString.ESCAPE_CHARACTER) {
                Result<char> characterResult = ReadEscapeCharacter();
                if (!characterResult.Successful) {
                    return Result<string>.Failure(characterResult);
                }
                stringBuilder.Append(characterResult.Value);
                continue;
            }
            stringBuilder.Append(c);
        }

        return Result<string>.Failure(new Message("Unterminated string"));
    }

    private Result<char> ReadEscapeCharacter() {
        if (!Reader.CanRead()) {
            return Result<char>.Failure(new Message("Unterminated escape sequence"));
        }

        char c = Reader.Read();
        if (JsonString.ESCAPE_CHARACTERS.TryGetValue(c, out char escapedCharacter)) {
            return Result<char>.Success(escapedCharacter);
        }
        if (c == JsonString.UNICODE_ESCAPE_CHARACTER) {
            return ReadUnicodeCharacter();
        }

        return Result<char>.Failure(new Message("Invalid escape sequence"));
    }

    private Result<char> ReadUnicodeCharacter() {
        if (!Reader.CanRead(4)) {
            return Result<char>.Failure(new Message("Unterminated escape sequence"));
        }
        string unicode = Reader.Read(4);
        if (ushort.TryParse(unicode, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort character)) {
            return Result<char>.Success((char)character);
        }
        return Result<char>.Failure(new Message("Invalid escape sequence"));
    }

    private Result<IJsonElement> ReadElement() {
        string literal = ReadLiteral();
        if (JsonNumber.TryParse(literal, out JsonNumber? jsonNumber)) {
            return Result<IJsonElement>.Success(jsonNumber);
        }
        if (JsonBoolean.TryParse(literal, out JsonBoolean? jsonBoolean)) {
            return Result<IJsonElement>.Success(jsonBoolean);
        }
        if (JsonNull.TryParse(literal, out JsonNull? jsonNull)) {
            return Result<IJsonElement>.Success(jsonNull);
        }
        return Result<IJsonElement>.Failure(new Message("Unable to parse JSON"));
    }

    private string ReadLiteral() {
        int start = Reader.Cursor;
        while (Reader.IsNext(IsLiteral)) {
            Reader.Skip();
        }
        return Reader.Text[start..Reader.Cursor];
    }

    private void SkipWhitespace() {
        while (Reader.IsNext(IsWhitespace)) {
            Reader.Skip();
        }
    }

    private static bool IsLiteral(char c) {
        return c != '{' && c != '}'
            && c != '[' && c != ']'
            && c != ':' && c != ','
            && !IsWhitespace(c);
    }

    private static bool IsWhitespace(char c) {
        return c == ' ' || c == '\t' || c == '\r' || c == '\n';
    }
}

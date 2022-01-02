using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ErrorCraft.Minecraft.Json;

public class JsonReader {
    private const string MALFORMED_JSON_MESSAGE = "Malformed JSON";
    private const string EXPECTED_NAME_SEPARATOR_MESSAGE = "Expected ':'";
    private const string UNTERMINATED_STRING_MESSAGE = "Unterminated string";
    private const string INVALID_ESCAPE_SEQUENCE_MESSAGE = "Invalid escape sequence";
    private const string UNTERMINATED_ESCAPE_SEQUENCE_MESSAGE = "Unterminated escape sequence";

    private readonly IStringReader Reader;

    public JsonReader(IStringReader reader) {
        Reader = reader;
    }

    public Result<IJsonElement> Read() {
        SkipWhitespace();
        if (Reader.IsNext(JsonObject.OBJECT_OPEN_CHARACTER)) {
            return ReadObject();
        }
        if (Reader.IsNext(JsonArray.ARRAY_OPEN_CHARACTER)) {
            return ReadArray();
        }
        if (Reader.IsNext(JsonString.QUOTE_CHARACTER)) {
            return Result<IJsonElement>.From(ReadString(), (value) => new JsonString(value));
        }
        return ReadElement();
    }

    private Result<IJsonElement> ReadObject() {
        if (!Reader.IsNext(JsonObject.OBJECT_OPEN_CHARACTER)) {
            return Result<IJsonElement>.Failure(new Message(MALFORMED_JSON_MESSAGE));
        }
        Reader.Skip();
        SkipWhitespace();
        if (!Reader.IsNext(JsonObject.OBJECT_CLOSE_CHARACTER)) {
            return ReadObjectContents();
        }
        Reader.Skip();
        return Result<IJsonElement>.Success(new JsonObject());
    }

    private Result<IJsonElement> ReadObjectContents() {
        Dictionary<string, IJsonElement> children = new Dictionary<string, IJsonElement>();
        while (Reader.CanRead()) {
            SkipWhitespace();
            Result<string> keyResult = ReadString();
            if (!keyResult.Successful) {
                return Result<IJsonElement>.Failure(keyResult);
            }

            SkipWhitespace();
            if (!Reader.IsNext(JsonObject.NAME_SEPARATOR)) {
                return Result<IJsonElement>.Failure(new Message(EXPECTED_NAME_SEPARATOR_MESSAGE));
            }
            Reader.Skip();

            SkipWhitespace();
            Result<IJsonElement> valueResult = Read();
            if (!valueResult.Successful) {
                return valueResult;
            }

            children.Add(keyResult.Value!, valueResult.Value!);

            SkipWhitespace();
            if (Reader.IsNext(JsonObject.VALUE_SEPARATOR)) {
                Reader.Skip();
                continue;
            }

            if (Reader.IsNext(JsonObject.OBJECT_CLOSE_CHARACTER)) {
                Reader.Skip();
                return Result<IJsonElement>.Success(new JsonObject(children));
            }
        }
        return Result<IJsonElement>.Failure(new Message(MALFORMED_JSON_MESSAGE));
    }

    private Result<IJsonElement> ReadArray() {
        if (!Reader.IsNext(JsonArray.ARRAY_OPEN_CHARACTER)) {
            return Result<IJsonElement>.Failure(new Message(MALFORMED_JSON_MESSAGE));
        }
        Reader.Skip();
        SkipWhitespace();
        if (!Reader.IsNext(JsonArray.ARRAY_CLOSE_CHARACTER)) {
            return ReadArrayContents();
        }
        Reader.Skip();
        return Result<IJsonElement>.Success(new JsonArray());
    }

    private Result<IJsonElement> ReadArrayContents() {
        List<IJsonElement> items = new List<IJsonElement>();
        while (Reader.CanRead()) {
            SkipWhitespace();
            Result<IJsonElement> itemResult = Read();
            if (!itemResult.Successful) {
                return itemResult;
            }

            items.Add(itemResult.Value!);

            SkipWhitespace();
            if (Reader.IsNext(JsonObject.VALUE_SEPARATOR)) {
                Reader.Skip();
                continue;
            }

            if (Reader.IsNext(JsonArray.ARRAY_CLOSE_CHARACTER)) {
                Reader.Skip();
                return Result<IJsonElement>.Success(new JsonArray(items));
            }
        }
        return Result<IJsonElement>.Failure(new Message(MALFORMED_JSON_MESSAGE));
    }

    private Result<string> ReadString() {
        if (!Reader.IsNext(JsonString.QUOTE_CHARACTER)) {
            return Result<string>.Failure(new Message(MALFORMED_JSON_MESSAGE));
        }
        Reader.Skip();
        return ReadCharacters();
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

        return Result<string>.Failure(new Message(UNTERMINATED_STRING_MESSAGE));
    }

    private Result<char> ReadEscapeCharacter() {
        if (!Reader.CanRead()) {
            return Result<char>.Failure(new Message(UNTERMINATED_ESCAPE_SEQUENCE_MESSAGE));
        }

        char c = Reader.Read();
        if (JsonString.ESCAPE_CHARACTERS.TryGetValue(c, out char escapedCharacter)) {
            return Result<char>.Success(escapedCharacter);
        }
        if (c == JsonString.UNICODE_ESCAPE_CHARACTER) {
            return ReadUnicodeCharacter();
        }

        return Result<char>.Failure(new Message(INVALID_ESCAPE_SEQUENCE_MESSAGE));
    }

    private Result<char> ReadUnicodeCharacter() {
        if (!Reader.CanRead(4)) {
            return Result<char>.Failure(new Message(UNTERMINATED_ESCAPE_SEQUENCE_MESSAGE));
        }
        string unicode = Reader.Read(4);
        if (ushort.TryParse(unicode, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort character)) {
            return Result<char>.Success((char)character);
        }
        return Result<char>.Failure(new Message(INVALID_ESCAPE_SEQUENCE_MESSAGE));
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
        return Result<IJsonElement>.Failure(new Message(MALFORMED_JSON_MESSAGE));
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

using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;

namespace ErrorCraft.Minecraft.Json;

public class JsonReader {
    private readonly IStringReader Reader;

    public JsonReader(IStringReader reader) {
        Reader = reader;
    }

    public Result<IJsonElement> Read() {
        SkipWhitespace();
        return ReadElement();
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

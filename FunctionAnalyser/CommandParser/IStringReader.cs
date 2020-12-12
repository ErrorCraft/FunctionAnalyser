using CommandParser.Results;

namespace CommandParser
{
    public interface IStringReader
    {
        IStringReader Copy();

        string GetString();
        int GetRemainingLength();
        int GetLength();
        int GetCursor();
        void SetCursor(int cursor);
        string GetRemaining();

        bool CanRead();
        bool CanRead(int length);
        char Read();
        string Read(int length);
        void Skip();
        void Skip(int length);
        void SkipWhitespace();
        char Peek();
        char Peek(int offset);
        bool AtEndOfArgument();
        bool IsQuotedStringStart(char c);

        ReadResults ReadInteger(out int value);
        ReadResults ReadLong(out long value);
        ReadResults ReadFloat(out float value);
        ReadResults ReadDouble(out double value);
        ReadResults ReadBoolean(out bool result);
        ReadResults ReadString(out string result);
        ReadResults ReadUnquotedString(out string result);
        ReadResults ReadQuotedString(out string result);
        ReadResults ReadStringUntil(char terminator, out string result);
        ReadResults Expect(char c);
    }
}

using ErrorCraft.CommandParser.Results;
using System;

namespace ErrorCraft.CommandParser {
    public interface IStringReader {
        string GetString();
        int GetCursor();
        void SetCursor(int cursor);
        bool CanRead();
        bool CanRead(int length);
        char Peek();
        char Read();
        void Skip();
        void Skip(int length);
        bool IsNext(char c);
        bool IsNext(Predicate<char> predicate);

        ParseResults ReadBoolean(out bool result);
    }
}

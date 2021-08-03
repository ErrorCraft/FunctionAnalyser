using ErrorCraft.CommandParser.Results;

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

        ParseResults ReadBoolean(out bool result);
    }
}

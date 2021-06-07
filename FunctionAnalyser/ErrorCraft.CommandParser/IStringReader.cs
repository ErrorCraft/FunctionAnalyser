﻿using ErrorCraft.CommandParser.Results;

namespace ErrorCraft.CommandParser {
    public interface IStringReader {
        string GetString();
        int GetCursor();
        bool CanRead();
        bool CanRead(int length);
        char Peek();
        char Read();
        void Skip();

        ReadResults ReadBoolean(out bool result);
    }
}

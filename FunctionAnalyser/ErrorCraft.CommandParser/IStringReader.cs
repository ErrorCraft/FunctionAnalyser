﻿namespace ErrorCraft.CommandParser {
    public interface IStringReader {
        string GetString();
        int GetCursor();
        bool CanRead();
        bool CanRead(int length);
    }
}

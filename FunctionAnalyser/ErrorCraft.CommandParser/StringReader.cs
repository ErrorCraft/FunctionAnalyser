﻿namespace ErrorCraft.CommandParser {
    public class StringReader : IStringReader {
        private readonly string Command;
        private int Cursor;

        public StringReader(string command) {
            Command = command;
            Cursor = 0;
        }

        public string GetString() {
            return Command;
        }

        public int GetCursor() {
            return Cursor;
        }
    }
}

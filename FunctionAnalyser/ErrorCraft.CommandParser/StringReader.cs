using ErrorCraft.CommandParser.Results;
using System;

namespace ErrorCraft.CommandParser {
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

        public bool CanRead() {
            return CanRead(1);
        }

        public bool CanRead(int length) {
            return Cursor + length <= Command.Length;
        }

        public char Peek() {
            return Command[Cursor];
        }

        public char Read() {
            return Command[Cursor++];
        }

        public void Skip() {
            Cursor++;
        }

        public ReadResults ReadBoolean(out bool result) {
            int start = Cursor;
            while (CanRead() && IsUnquotedStringPart(Peek())) {
                Skip();
            }
            if (bool.TryParse(Command[start..Cursor], out result)) {
                return ReadResults.Success();
            }
            return ReadResults.Failure();
        }

        private static bool IsUnquotedStringPart(char c) {
            return c >= 'A' && c <= 'Z'
                || c >= 'a' && c <= 'z';
        }
    }
}

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

        public void SetCursor(int cursor) {
            Cursor = cursor;
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

        public void Skip(int length) {
            Cursor += length;
        }

        public bool IsNext(char c) {
            return CanRead() && Peek() == c;
        }

        public bool IsNext(Predicate<char> predicate) {
            return CanRead() && predicate(Peek());
        }

        public ParseResults ReadBoolean(out bool result) {
            int start = Cursor;
            while (IsNext(IsUnquotedStringPart)) {
                Skip();
            }
            if (bool.TryParse(Command[start..Cursor], out result)) {
                return ParseResults.Success();
            }
            return ParseResults.Failure(CommandError.InvalidBoolean());
        }

        public ParseResults ReadInteger(out int result) {
            int start = Cursor;
            while (IsNext(IsNumberPart)) {
                Skip();
            }
            if (start == Cursor) {
                result = 0;
                return ParseResults.Failure(CommandError.ExpectedInteger());
            }
            string number = Command[start..Cursor];
            if (!int.TryParse(number, out result)) {
                return ParseResults.Failure(CommandError.InvalidInteger(number));
            }
            return ParseResults.Success();
        }

        private static bool IsUnquotedStringPart(char c) {
            return c >= 'A' && c <= 'Z'
                || c >= 'a' && c <= 'z';
        }

        private static bool IsNumberPart(char c) {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }
    }
}

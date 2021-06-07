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
    }
}

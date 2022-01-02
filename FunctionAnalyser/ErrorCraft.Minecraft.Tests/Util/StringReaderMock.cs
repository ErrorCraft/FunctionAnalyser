using ErrorCraft.Minecraft.Util;

namespace ErrorCraft.Minecraft.Tests.Util;

internal class StringReaderMock : IStringReader {
    public string Text { get; }
    public int Cursor { get; set; }

    public StringReaderMock(string text) {
        Text = text;
        Cursor = 0;
    }

    public bool CanRead() {
        return CanRead(1);
    }

    public bool CanRead(int length) {
        return Cursor + length <= Text.Length;
    }

    public char Read() {
        return Text[Cursor++];
    }

    public string Read(int length) {
        int start = Cursor;
        Cursor += length;
        return Text.Substring(start, length);
    }

    public char Peek() {
        return Text[Cursor];
    }

    public void Skip() {
        Cursor++;
    }
}

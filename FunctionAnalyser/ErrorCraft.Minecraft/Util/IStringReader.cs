using System;

namespace ErrorCraft.Minecraft.Util;

public interface IStringReader {
    string Text { get; }
    int Cursor { get; set; }

    bool CanRead();
    bool CanRead(int length);
    char Read();
    char Peek();
    void Skip();
    
    bool IsNext(char c) {
        return CanRead() && Peek() == c;
    }

    bool IsNext(Predicate<char> predicate) {
        return CanRead() && predicate(Peek());
    }
}

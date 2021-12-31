using System;

namespace ErrorCraft.Minecraft.Util;

public interface IStringReader {
    bool CanRead();
    char Read();
    char Peek();
}

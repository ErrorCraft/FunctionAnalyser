namespace AdvancedText
{
    public interface IWriter
    {
        void Write(TextComponent textComponent);
        void WriteLine(TextComponent textComponent);
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();
        void Reset();
    }
}

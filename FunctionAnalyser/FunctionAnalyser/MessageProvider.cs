using AdvancedText;
using CommandParser;

namespace FunctionAnalyser
{
    public static class MessageProvider
    {
        public static TextComponent Error(int line, CommandError error)
        {
            return new TextComponent($"  Line {line}: {error.GetMessage()}").WithColour(Colour.BuiltinColours.RED);
        }

        public static TextComponent ErrorsFound(int errorCount, string path, bool skip)
        {
            return new TextComponent(errorCount == 1 ? $"Error found in " : $"{errorCount} errors found in ").WithColour(Colour.BuiltinColours.RED).With(
                new TextComponent(path).With(
                    skip ? new TextComponent($", skipping").WithColour(Colour.BuiltinColours.RED) : null
                    )
                );
        }

        public static TextComponent Empty(string path)
        {
            return new TextComponent($"Empty function found at ").WithColour(Colour.BuiltinColours.YELLOW).With(
                new TextComponent(path)
                );
        }
    }
}

using AdvancedText;
using CommandParser;
using FunctionAnalyser.Results;

namespace FunctionAnalyser
{
    public static class MessageProvider
    {
        public static TextComponent Empty(string path)
        {
            return new TextComponent($"Empty function found at ").WithColour(Colour.BuiltinColours.YELLOW)
                .With(new TextComponent(path));
        }

        public static TextComponent ErrorsFound(int errorCount, string path, bool skip)
        {
            return new TextComponent(errorCount == 1 ? $"Error found in " : $"{errorCount} errors found in ").WithColour(Colour.BuiltinColours.RED)
                .With(new TextComponent(path)
                .With(skip ? new TextComponent($", skipping function").WithColour(Colour.BuiltinColours.RED) : null));
        }

        public static TextComponent Error(int line, CommandError error)
        {
            return new TextComponent($"  Line {line}: {error.GetMessage()}").WithColour(Colour.BuiltinColours.RED);
        }

        public static TextComponent Message(string message)
        {
            return new TextComponent($"  {message}").WithColour(Colour.BuiltinColours.GREEN);
        }

        public static TextComponent Result(string message, IResult result)
        {
            return new TextComponent($"  {message}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(result.ToTextComponent());
        }

        public static TextComponent Result(string message, IResult result, int maximum)
        {
            return new TextComponent($"  {message}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(result.ToTextComponent()
                .With(Percentage(result.GetTotal(), maximum, " of total lines")));
        }

        public static TextComponent CommandResult(string command, Command usage)
        {
            return new TextComponent($"    {command}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(new TextComponent(usage.Commands.ToString())
                .With(usage.BehindExecute > 0 ? new TextComponent($" ({usage.BehindExecute} behind execute)").WithColour(Colour.BuiltinColours.GREY) : null));
        }

        public static TextComponent EntitySelector(char selector, int numberOfSelectors)
        {
            return new TextComponent($"    @{selector}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(new TextComponent(numberOfSelectors.ToString()));
        }

        private static TextComponent Percentage(int portion, int total, string suffix)
        {
            return total > 0 ? new TextComponent($" ({(double)portion / total:#0.00%}{suffix ?? ""})").WithColour(Colour.BuiltinColours.GREY) : null;
        }
    }
}

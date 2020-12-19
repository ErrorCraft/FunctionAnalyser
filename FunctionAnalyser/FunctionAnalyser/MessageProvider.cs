using AdvancedText;
using CommandParser;
using FunctionAnalyser.Results;
using System.Text;

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
            return new TextComponent($"\n  Line {line}: {error.GetMessage()}").WithColour(Colour.BuiltinColours.RED);
        }

        public static TextComponent Message(string message)
        {
            return new TextComponent($"  {message}\n").WithColour(Colour.BuiltinColours.GREEN);
        }

        public static TextComponent Result(string message, IResult result)
        {
            return new TextComponent($"  {message}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(result.ToTextComponent().With(new TextComponent("\n")));
        }

        public static TextComponent Result(string message, IResult result, int maximum)
        {
            return new TextComponent($"  {message}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(result.ToTextComponent()
                .With(Percentage(result.GetTotal(), maximum, " of total lines") ?? new TextComponent("\n")));
        }

        public static TextComponent CommandResult(string command, Command usage)
        {
            return new TextComponent($"    {command}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(new TextComponent(usage.Commands.ToString())
                .With(new TextComponent(usage.BehindExecute > 0 ? $" ({usage.BehindExecute} behind execute)\n" : "\n").WithColour(Colour.BuiltinColours.GREY)));
        }

        public static TextComponent EntitySelector(char selector, int numberOfSelectors, int totalFunctions, int totalCommands)
        {
            return new TextComponent($"    @{selector}: ").WithColour(Colour.BuiltinColours.GREEN)
                .With(new TextComponent(numberOfSelectors.ToString())
                .With(Average(numberOfSelectors, totalFunctions, totalCommands) ?? new TextComponent("\n")));
        }

        private static TextComponent Percentage(int portion, int total, string suffix)
        {
            return total > 0 ? new TextComponent($" ({(double)portion / total:#0.00%}{suffix ?? ""})\n").WithColour(Colour.BuiltinColours.GREY) : new TextComponent("\n");
        }

        private static TextComponent Average(int item, int totalFunctions, int totalCommands)
        {
            if (totalFunctions == 0) return null;

            StringBuilder stringBuilder = new StringBuilder(" (Average: ");
            stringBuilder.Append($"{(double)item / totalFunctions:#0.00} per function");

            if (totalCommands > 0)
            {
                stringBuilder.Append($", {(double)item / totalCommands:#0.00} per command");
            }

            stringBuilder.Append(")\n");
            return new TextComponent(stringBuilder.ToString()).WithColour(Colour.BuiltinColours.GREY);
        }
    }
}

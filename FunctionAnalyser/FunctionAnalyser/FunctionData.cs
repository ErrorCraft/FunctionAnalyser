using AdvancedText;
using FunctionAnalyser.Results;
using System.Collections.Generic;

namespace FunctionAnalyser
{
    public class FunctionData
    {
        public int Functions { get; set; }
        public int Commands { get; set; }
        public int Comments { get; set; }
        public int EmptyLines { get; set; }
        public SelectorCount Selectors { get; set; }

        public CommandUsage UsedCommands { get; set; }

        public List<TextComponent> Messages { get; set; }

        public FunctionData()
        {
            Messages = new List<TextComponent>();
            UsedCommands = new CommandUsage();
            Selectors = new SelectorCount();
        }

        public static FunctionData operator +(FunctionData a, FunctionData b)
        {
            a.Functions += b.Functions;
            a.Commands += b.Commands;
            a.Comments += b.Comments;
            a.EmptyLines += b.EmptyLines;
            a.UsedCommands.Merge(b.UsedCommands);
            a.Selectors += b.Selectors;

            return a;
        }
    }
}

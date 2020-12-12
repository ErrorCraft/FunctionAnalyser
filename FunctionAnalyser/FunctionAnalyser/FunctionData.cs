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
        public int FunctionCalls { get; set; }

        public List<TextComponent> Messages { get; set; }

        public FunctionData()
        {
            Messages = new List<TextComponent>();
            UsedCommands = new CommandUsage();
            Selectors = new SelectorCount();
        }

        public static FunctionData operator +(FunctionData a, FunctionData b)
        {
            return new FunctionData()
            {
                Functions = a.Functions + b.Functions,
                Commands = a.Commands + b.Commands,
                Comments = a.Comments + b.Comments,
                EmptyLines = a.EmptyLines + b.EmptyLines,
                UsedCommands = new CommandUsage().Merge(a.UsedCommands).Merge(b.UsedCommands),
                Selectors = a.Selectors + b.Selectors,
                FunctionCalls = a.Functions + b.Functions
            };
        }
    }
}

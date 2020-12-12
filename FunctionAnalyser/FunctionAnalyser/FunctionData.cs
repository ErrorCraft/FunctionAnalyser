using AdvancedText;
using FunctionAnalyser.Results;
using System.Collections.Generic;

namespace FunctionAnalyser
{
    public class FunctionData
    {
        public GenericResult Functions { get; private init; } = new GenericResult();
        public GenericResult Comments { get; private init; } = new GenericResult();
        public GenericResult EmptyLines { get; private init; } = new GenericResult();
        public GenericResult Commands { get; private init; } = new GenericResult();
        public CommandUsage UsedCommands { get; private init; } = new CommandUsage();

        public SelectorCount Selectors { get; private init; } = new SelectorCount();
        public GenericResult FunctionCalls { get; private init; } = new GenericResult();
        public GenericSelectorResult PredicateCalls { get; private init; } = new GenericSelectorResult();

        public List<TextComponent> Messages { get; } = new List<TextComponent>();

        public static FunctionData operator +(FunctionData a, FunctionData b)
        {
            return new FunctionData()
            {
                Functions = a.Functions + b.Functions,
                Comments = a.Comments + b.Comments,
                EmptyLines = a.EmptyLines + b.EmptyLines,
                Commands = a.Commands + b.Commands,
                UsedCommands = new CommandUsage().Merge(a.UsedCommands).Merge(b.UsedCommands),
                Selectors = a.Selectors + b.Selectors,
                FunctionCalls = a.Functions + b.Functions,
                PredicateCalls = a.PredicateCalls + b.PredicateCalls
            };
        }
    }
}

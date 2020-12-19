using AdvancedText;
using FunctionAnalyser.Results;
using System.Collections.Generic;

namespace FunctionAnalyser
{
    public class FunctionData
    {
        public SimpleResult Functions { get; private init; } = new SimpleResult();
        public SimpleResult Comments { get; private init; } = new SimpleResult();
        public SimpleResult EmptyLines { get; private init; } = new SimpleResult();
        public SimpleResult Commands { get; private init; } = new SimpleResult();
        public CommandUsage UsedCommands { get; private init; } = new CommandUsage();

        public SelectorCount Selectors { get; private init; } = new SelectorCount();
        public SimpleResult FunctionCalls { get; private init; } = new SimpleResult();
        public SelectorResult PredicateCalls { get; private init; } = new SelectorResult();
        public SelectorResult NbtAccess { get; private init; } = new SelectorResult();
        public SimpleResult StorageUsage { get; private init; } = new SimpleResult();
        public SimpleResult LootTableUsage { get; private init; } = new SimpleResult();
        public SimpleResult ItemModifierUsage { get; private init; } = new SimpleResult();
        public SimpleResult AttributeUsage { get; private init; } = new SimpleResult();

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
                PredicateCalls = a.PredicateCalls + b.PredicateCalls,
                NbtAccess = a.NbtAccess + b.NbtAccess,
                StorageUsage = a.StorageUsage + b.StorageUsage,
                LootTableUsage = a.LootTableUsage+ b.LootTableUsage,
                ItemModifierUsage = a.ItemModifierUsage+ b.ItemModifierUsage,
                AttributeUsage = a.AttributeUsage + b.AttributeUsage,
            };
        }
    }
}

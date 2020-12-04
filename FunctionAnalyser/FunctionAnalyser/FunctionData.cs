using AdvancedText;
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

        public Dictionary<string, CommandUsage> UsedCommands { get; set; }

        public List<TextComponent> Messages { get; set; }

        public FunctionData()
        {
            Messages = new List<TextComponent>();
            UsedCommands = new Dictionary<string, CommandUsage>();
            Selectors = new SelectorCount();
        }

        public static FunctionData operator +(FunctionData a, FunctionData b)
        {
            a.Functions += b.Functions;
            a.Commands += b.Commands;
            a.Comments += b.Comments;
            a.EmptyLines += b.EmptyLines;
            foreach (KeyValuePair<string, CommandUsage> kvp in b.UsedCommands)
            {
                if (a.UsedCommands.ContainsKey(kvp.Key)) a.UsedCommands[kvp.Key] += kvp.Value;
                else a.UsedCommands[kvp.Key] = kvp.Value;
            }
            a.Selectors += b.Selectors;

            return a;
        }
    }
}

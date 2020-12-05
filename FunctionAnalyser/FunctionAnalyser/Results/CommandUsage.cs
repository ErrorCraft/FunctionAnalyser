using System.Collections.Generic;

namespace FunctionAnalyser.Results
{
    public class CommandUsage
    {
        private readonly Dictionary<string, Command> Commands;

        public CommandUsage()
        {
            Commands = new Dictionary<string, Command>();
        }

        public void Increase(string command, bool behindExecute)
        {
            if (!Commands.ContainsKey(command)) Commands[command] = new Command();

            Commands[command].Commands++;
            if (behindExecute) Commands[command].BehindExecute++;
        }

        public void Merge(CommandUsage other)
        {
            foreach (KeyValuePair<string, Command> otherPair in other.Commands)
            {
                if (Commands.ContainsKey(otherPair.Key)) Commands[otherPair.Key] += otherPair.Value;
                else Commands[otherPair.Key] = otherPair.Value;
            }
        }

        public Dictionary<string, Command> GetSorted(SortType sortType)
        {
            List<string> keys = new List<string>(Commands.Keys);
            switch (sortType)
            {
                case SortType.CommandLength:
                    keys.Sort((a, b) => -a.Length.CompareTo(b.Length));
                    break;
                case SortType.Alphabetical:
                    keys.Sort((a, b) => a.CompareTo(b));
                    break;
                case SortType.TimesUsed:
                default:
                    keys.Sort((a, b) => -Commands[a].Commands.CompareTo(Commands[b].Commands));
                    break;
            }

            Dictionary<string, Command> newValues = new Dictionary<string, Command>();
            for (int i = 0; i < keys.Count; i++)
            {
                newValues.Add(keys[i], Commands[keys[i]]);
            }
            return newValues;
        }
    }
}

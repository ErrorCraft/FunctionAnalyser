using FunctionAnalyser;
using System.Collections.Generic;

namespace UserInterface
{
    public class CommandVersionViewModel
    {
        private readonly string CommandName;
        private readonly string FancyName;

        public CommandVersionViewModel(string commandName, string fancyName)
        {
            CommandName = commandName;
            FancyName = fancyName;
        }

        public string GetCommandName()
        {
            return CommandName;
        }

        public override string ToString()
        {
            return FancyName;
        }

        public static List<CommandVersionViewModel> FromVersionNames(List<VersionName> versionNames)
        {
            List<CommandVersionViewModel> versionViewModels = new List<CommandVersionViewModel>();
            foreach (VersionName versionName in versionNames)
            {
                versionViewModels.Add(new CommandVersionViewModel(versionName.CommandName, versionName.FancyName));
            }
            return versionViewModels;
        }
    }
}

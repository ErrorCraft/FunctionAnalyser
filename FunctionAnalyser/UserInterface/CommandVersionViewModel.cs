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
    }
}

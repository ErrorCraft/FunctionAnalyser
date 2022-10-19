namespace ErrorCraft.PackAnalyser {
    public class VersionName {
        public string CommandName { get; }
        public string FancyName { get; }

        public VersionName(string commandName, string fancyName) {
            CommandName = commandName;
            FancyName = fancyName;
        }
    }
}

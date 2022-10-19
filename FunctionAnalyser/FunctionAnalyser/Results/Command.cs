namespace ErrorCraft.PackAnalyser.Results {
    public class Command {
        public int Commands { get; set; }
        public int BehindExecute { get; set; }

        public static Command operator +(Command a, Command b) {
            return new Command() {
                Commands = a.Commands + b.Commands,
                BehindExecute = a.BehindExecute + b.BehindExecute
            };
        }
    }
}

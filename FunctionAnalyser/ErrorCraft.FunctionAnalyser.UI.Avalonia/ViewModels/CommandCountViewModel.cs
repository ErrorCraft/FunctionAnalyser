namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;

public class CommandCountViewModel : ViewModelBase {
    public string Command { get; }
    public int Count { get; }
    public int ExecuteCount { get; }

    public CommandCountViewModel(string command, int count, int executeCount) {
        Command = command;
        Count = count;
        ExecuteCount = executeCount;
    }
}

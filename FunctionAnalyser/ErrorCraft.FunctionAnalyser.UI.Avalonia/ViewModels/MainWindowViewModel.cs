using Avalonia.Controls.Documents;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public InlineCollection Output { get; } = new InlineCollection();
}

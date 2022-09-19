using Avalonia.Controls.Documents;
using System.Collections.ObjectModel;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public InlineCollection Output { get; } = new InlineCollection();
    public ObservableCollection<CommandCountViewModel> Commands { get; } = new ObservableCollection<CommandCountViewModel>();
}

using Avalonia.Controls.Documents;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private double analyseProgress = 0.0d;

    public InlineCollection Output { get; } = new InlineCollection();
    public ObservableCollection<CommandCountViewModel> Commands { get; } = new ObservableCollection<CommandCountViewModel>();
    public double AnalyseProgress {
        get { return analyseProgress; }
        set { this.RaiseAndSetIfChanged(ref analyseProgress, value); }
    }
}

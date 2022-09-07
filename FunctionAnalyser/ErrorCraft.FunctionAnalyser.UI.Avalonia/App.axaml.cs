using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;
using ErrorCraft.FunctionAnalyser.UI.Avalonia.Views;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia;
public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

using Avalonia;
using Avalonia.Controls;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.Controls;
public class TitledPanel : ItemsControl {
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<TitledPanel, string>(nameof(Title));

    public string Title {
        get { return GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
}

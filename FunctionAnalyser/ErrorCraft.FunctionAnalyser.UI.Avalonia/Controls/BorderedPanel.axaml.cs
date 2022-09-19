using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.Controls;

public class BorderedPanel : ItemsControl {
    public static readonly StyledProperty<IBrush?> BackgroundColourProperty = AvaloniaProperty.Register<BorderedPanel, IBrush?>(nameof(BackgroundColour));
    public static readonly StyledProperty<IBrush?> ShadowColourProperty = AvaloniaProperty.Register<BorderedPanel, IBrush?>(nameof(ShadowColour));
    public static readonly StyledProperty<bool> HasEdgeProperty = AvaloniaProperty.Register<BorderedPanel, bool>(nameof(HasEdge), false);

    public IBrush? BackgroundColour {
        get { return GetValue(BackgroundColourProperty); }
        set { SetValue(BackgroundColourProperty, value); }
    }

    public IBrush? ShadowColour {
        get { return GetValue(ShadowColourProperty); }
        set { SetValue(ShadowColourProperty, value); }
    }

    public bool HasEdge {
        get { return GetValue(HasEdgeProperty); }
        set { SetValue(HasEdgeProperty, value); }
    }
}

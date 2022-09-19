using Avalonia.Data.Converters;
using Avalonia.Media;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.Converters;

public static class BorderedPanelBoxShadowConverter {
    public static readonly IValueConverter Converter = new FuncValueConverter<ISolidColorBrush?, BoxShadows>(Convert);

    private static BoxShadows Convert(ISolidColorBrush? value) {
        Color colour = value?.Color ?? Colors.Black;
        BoxShadow boxShadow = new BoxShadow() {
            IsInset = true,
            OffsetX = 0.0d,
            OffsetY = 1.0d,
            Blur = 5.0d,
            Color = colour
        };
        return new BoxShadows(boxShadow);
    }
}

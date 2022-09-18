using AdvancedText;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia;
internal class AvaloniaLogger : ILogger {
    private readonly InlineCollection Output;

    public AvaloniaLogger(InlineCollection output) {
        Output = output;
    }

    public void Clear() {
        Output.Clear();
    }

    public string GetFlatString() {
        throw new NotImplementedException();
    }

    public void Log(TextComponent component) {
        Dispatcher.UIThread.Post(() => {
            Output.AddRange(GetRuns(component));
            Output.Add(new LineBreak());
        });
    }

    public void Log(List<TextComponent> component) {
        if (component.Count == 0) {
            return;
        }

        Dispatcher.UIThread.Post(() => {
            foreach (TextComponent text in component) {
                Output.AddRange(GetRuns(text));
            }
            Output.Add(new LineBreak());
        });
    }

    private static IEnumerable<Run> GetRuns(TextComponent text) {
        TextComponent actualText = text;
        while (actualText != null) {
            yield return GetRun(actualText);
            actualText = actualText.Child;
        }
    }

    private static Run GetRun(TextComponent text) {
        return new Run(text.Text) {
            Foreground = new SolidColorBrush(Color.FromRgb(text.Colour.Red, text.Colour.Green, text.Colour.Blue)),
            FontStyle = text.Italic ? FontStyle.Italic : FontStyle.Normal,
            FontWeight = text.Bold ? FontWeight.Bold : FontWeight.Normal
        };
    }
}

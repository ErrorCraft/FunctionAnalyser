using AdvancedText;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace UserInterface
{
    public class AnalyserLogger : ILogger
    {
        private readonly Dispatcher Disp;
        private readonly ObservableCollection<TextBlock> Objects;
        private string FlatString;

        public AnalyserLogger(ObservableCollection<TextBlock> objects, Dispatcher dispatcher)
        {
            Objects = objects;
            Disp = dispatcher;
        }

        public string GetFlatString()
        {
            return FlatString;
        }

        private IEnumerable<Run> GetRuns(TextComponent component, bool endWithNewLine)
        {
            TextComponent actualComponent = component;
            while (actualComponent != null)
            {
                yield return GetRun(actualComponent);
                actualComponent = actualComponent.Child;
            }
            if (endWithNewLine) yield return GetRun(new TextComponent("\n"));
        }

        private Run GetRun(TextComponent component)
        {
            FlatString += component.Text;
            return new Run(component.Text)
            {
                Foreground = new SolidColorBrush(Color.FromRgb(component.Colour.Red, component.Colour.Green, component.Colour.Blue)),
                FontStyle = component.Italic ? FontStyles.Italic : FontStyles.Normal,
                FontWeight = component.Bold ? FontWeights.Bold : FontWeights.Normal
            };
        }

        public void Clear()
        {
            Objects.Clear();
            FlatString = "";
        }

        public void Log(TextComponent component)
        {
            Log(new List<TextComponent>(1) { component });
        }

        public void Log(List<TextComponent> components)
        {
            if (components is null || components.Count == 0) return;

            try
            {
                Disp.Invoke(() =>
                {
                    TextBlock tb = new TextBlock();
                    for (int i = 0; i < components.Count - 1; i++)
                    {
                        tb.Inlines.AddRange(GetRuns(components[i], true));
                    }
                    tb.Inlines.AddRange(GetRuns(components[^1], false));
                    Objects.Add(tb);
                    FlatString += "\n";
                });
            } catch (TaskCanceledException) { }
        }
    }
}

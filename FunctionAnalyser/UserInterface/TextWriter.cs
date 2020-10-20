using AdvancedText;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace UserInterface
{
    class TextWriter : IWriter
    {
        private readonly TextBlock Output;
        private string Text;

        public TextWriter(TextBlock output)
        {
            Output = output;
            Text = "";
        }

        private static Run GetRun(TextComponent textComponent)
        {
            return new Run()
            {
                Text = textComponent.Text,
                Foreground = new SolidColorBrush(Color.FromRgb(textComponent.Colour.Red, textComponent.Colour.Green, textComponent.Colour.Blue)),
                FontStyle = textComponent.Italic ? FontStyles.Italic : FontStyles.Normal,
                FontWeight = textComponent.Bold ? FontWeights.Bold : FontWeights.Normal,
            };
        }

        public string GetFlatOutput() => Text;

        public void Write(TextComponent textComponent)
        {
            try
            {
                Output.Dispatcher.Invoke(() =>
                {
                    Output.Inlines.Add(GetRun(textComponent));
                    Text += textComponent.Text;
                });
            }
            catch (TaskCanceledException)
            {
                // empty
            }
        }

        public void WriteLine(TextComponent textComponent)
        {
            Output.Dispatcher.Invoke(() =>
            {
                Write(textComponent);
                Write("\n");
            });
        }

        public void Write(string text)
        {
            Output.Dispatcher.Invoke(() => Write(new TextComponent(text)));
        }

        public void WriteLine(string text)
        {
            Output.Dispatcher.Invoke(() => Write(new TextComponent(text + "\n")));
        }

        public void WriteLine()
        {
            Output.Dispatcher.Invoke(() => Write(new TextComponent("\n")));
        }

        public void Reset()
        {
            Output.Inlines.Clear();
            Text = "";
        }
    }
}

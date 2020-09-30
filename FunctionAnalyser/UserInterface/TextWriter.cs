using AdvancedText;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace UserInterface
{
    class TextWriter : IWriter
    {
        private readonly TextBlock Output;

        public TextWriter(TextBlock output)
        {
            Output = output;
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

        public void Write(TextComponent textComponent)
        {
            Output.Dispatcher.Invoke(() => Output.Inlines.Add(GetRun(textComponent)));
        }

        public void WriteLine(TextComponent textComponent)
        {
            Output.Dispatcher.Invoke(() =>
            {
                Write(textComponent);
                Write("\n");
            });
        }

        public void Write(TextComponent[] textComponent)
        {
            Output.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < textComponent.Length; i++)
                {
                    Write(textComponent[i]);
                }
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
    }
}

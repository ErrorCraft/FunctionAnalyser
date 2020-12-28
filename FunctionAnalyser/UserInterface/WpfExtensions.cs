using System.Windows;
using System.Windows.Controls;

namespace UserInterface
{
    public static class WpfExtensions
    {
        public static bool GetAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollProperty, value);
        }

        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(WpfExtensions), new PropertyMetadata(false, AutoScrollPropertyChanged));

        public static void AutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ScrollViewer scrollViewer)
            {
                if ((bool)args.NewValue)
                {
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                    scrollViewer.ScrollToEnd();
                } else
                {
                    scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
                }
            }
        }

        private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
            {
                var scrollViewer = sender as ScrollViewer;
                scrollViewer?.ScrollToBottom();
            }
        }
    }
}

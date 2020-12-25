using System;
using System.Diagnostics;
using System.Windows;

namespace UserInterface
{
    public partial class CrashWindow : Window
    {
        private readonly string CrashPath;

        public CrashWindow(Exception exception, string crashPath)
        {
            InitializeComponent();
            CrashDescription.Text = $"{exception.GetType().FullName}: {exception.Message}";
            CrashPath = crashPath;
        }

        private void ViewReport(object sender, RoutedEventArgs e)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(CrashPath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

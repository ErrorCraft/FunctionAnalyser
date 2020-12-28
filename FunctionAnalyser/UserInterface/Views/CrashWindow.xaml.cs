using System;
using System.Diagnostics;
using System.Windows;
using UserInterface.ViewModels;

namespace UserInterface
{
    public partial class CrashWindow : Window
    {
        private readonly CrashWindowViewModel Model;

        public CrashWindow(Exception exception, string crashReportFile)
        {
            InitializeComponent();
            DataContext = Model = new CrashWindowViewModel(exception, crashReportFile);
        }

        private void ViewReport(object sender, RoutedEventArgs e)
        {
            new Process() { StartInfo = new ProcessStartInfo(Model.CrashReportFile) { UseShellExecute = true } }.Start();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

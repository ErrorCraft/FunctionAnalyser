using ProgramUpdater;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UserInterface.ViewModels;

namespace UserInterface
{
    public partial class UpdateWindow : Window
    {
        private readonly Update NewVersion;
        private readonly UpdateWindowViewModel Model;

        public UpdateWindow(Update newVersion)
        {
            InitializeComponent();
            DataContext = Model = new UpdateWindowViewModel();
            NewVersion = newVersion;
        }

        private void CancelUpdate(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async void ConfirmUpdate(object sender, RoutedEventArgs e)
        {
            (string tempFilePath, string newFilePath) = await NewVersion.Download();
            UpdateApplication(tempFilePath, newFilePath);
            Environment.Exit(0);
        }

        private static void UpdateApplication(string tempFilePath, string newFilePath)
        {
            string argument = "/C Choice /C Y /N /D Y /T 1 & Del /F /Q \"{0}\" & Start \"\" /D \"{1}\" \"{2}\"";

            ProcessStartInfo info = new ProcessStartInfo
            {
                Arguments = string.Format(argument, tempFilePath, Path.GetDirectoryName(newFilePath), Path.GetFileName(newFilePath)),
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(info);
        }

        private void LoadedWindow(object sender, RoutedEventArgs e)
        {
            Model.Version = NewVersion.UpdateVersion.ToString();
            Model.Changelog = NewVersion.Changelog;
        }
    }
}

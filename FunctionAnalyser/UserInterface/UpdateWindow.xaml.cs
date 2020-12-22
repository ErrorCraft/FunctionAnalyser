using CommandFilesApi;
using CommandFilesApi.GitHub;
using System.Linq;
using System.Windows;

namespace UserInterface
{
    public partial class UpdateWindow : Window
    {
        private readonly Update NewVersion;

        public UpdateWindow(Update newVersion)
        {
            InitializeComponent();
            NewVersion = newVersion;
        }

        private void CancelUpdate(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ConfirmUpdate(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void LoadedWindow(object sender, RoutedEventArgs e)
        {
            UpdateValueLabel.Content = NewVersion.UpdateVersion.ToString();
            ContentLabel.Content = NewVersion.Changelog;
        }
    }
}

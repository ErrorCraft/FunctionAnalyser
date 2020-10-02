using AdvancedText;
using FunctionAnalyser;
using Microsoft.WindowsAPICodePack.Dialogs;
using IO = System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommandFilesApi;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly TextWriter Writer;
        public MainWindow()
        {
            InitializeComponent();
            Writer = new TextWriter(Output);
            TextComponent.SetDefaultColour(Colour.BuiltinColours.GREY);
            ApiHelper.Initialise();
        }

        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog select = new CommonOpenFileDialog("Select a folder") { IsFolderPicker = true };
            if (select.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Folder.Content = select.FileName;
            }
        }

        private async void ExportResults(object sender, RoutedEventArgs e)
        {
            CommonSaveFileDialog save = new CommonSaveFileDialog("Export results") { DefaultExtension = "txt", AlwaysAppendDefaultExtension = true };
            if (save.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await IO::File.WriteAllTextAsync(save.FileName, Writer.GetFlatOutput());
                MessageBox.Show("Exported!");
            }
        }

        private async void ReadFiles(object sender, RoutedEventArgs e)
        {
            AnalyseButton.IsEnabled = false;
            ExportButton.IsEnabled = false;
            FolderButton.IsEnabled = false;

            // Clear output
            Writer.Reset();

            // Read functions
            FunctionReader functionReader = new FunctionReader(Folder.Content.ToString(), Writer);

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Task task = tcs.Task;
            Thread functionThread = new Thread(() => {
                try
                {
                    functionReader.ReadAllFunctions("java");
                }
                catch (TaskCanceledException) { }
                finally
                {
                    tcs.SetResult(true);
                }
            });

            functionThread.Start();
            await Task.WhenAll(task);

            AnalyseButton.IsEnabled = true;
            ExportButton.IsEnabled = true;
            FolderButton.IsEnabled = true;
        }

        private async void GetFiles(object sender, RoutedEventArgs e)
        {
            string version = await FileProcessor.LoadFile("version.json");
            Writer.WriteLine(version);
        }
    }
}

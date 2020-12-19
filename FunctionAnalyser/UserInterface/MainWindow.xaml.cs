using AdvancedText;
using CommandFilesApi;
using FunctionAnalyser;
using FunctionAnalyser.Results;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowModel Model;
        private readonly ILogger Logger;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Model = new MainWindowModel();
            Logger = new AnalyserLogger(Model.Blocks, Output.Dispatcher);
            ApiHelper.Initialise();
        }

        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog select = new CommonOpenFileDialog("Select a folder") { IsFolderPicker = true };
            if (select.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Model.FolderPath = select.FileName;
                Model.AnalyseEnabled = true;
            }
        }

        private async void ExportResults(object sender, RoutedEventArgs e)
        {
            CommonSaveFileDialog save = new CommonSaveFileDialog("Export results") { DefaultExtension = "txt", AlwaysAppendDefaultExtension = true };
            save.Filters.Add(new CommonFileDialogFilter("Text Files", "*.txt"));
            if (save.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await File.WriteAllTextAsync(save.FileName, Logger.GetFlatString());
            }
        }

        private async void ReadFiles(object sender, RoutedEventArgs e)
        {
            Model.DisableOptions();
            Model.DisableButtons();
            Logger.Clear();

            Progress<FunctionProgress> progress = new Progress<FunctionProgress>();
            progress.ProgressChanged += ReportProgress;

            FunctionOptions options = new FunctionOptions()
            {
                SkipFunctionOnError = Model.SkipFunctionOnError,
                ShowCommandErrors = Model.ShowCommandErrors,
                ShowEmptyFunctions = Model.ShowEmptyFunctions,
                CommandSort = SortType.Alphabetical
            };
            FunctionReader functionReader = new FunctionReader(Model.FolderPath, Logger, progress, options);
            await Task.Run(() => functionReader.Analyse("java"));

            Model.EnableOptions();
            Model.EnableButtons();
        }

        private void ReportProgress(object sender, FunctionProgress e)
        {
            Progress.Value = e.Completion;
        }

        private async void LoadedWindow(object sender, RoutedEventArgs e)
        {
            FileProcessor fileProcessor = new FileProcessor(Logger);
            try
            {
                await Task.Run(fileProcessor.GetFiles);
                Model.SelectFolderEnabled = true;
                Model.EnableOptions();
            } catch (HttpRequestException)
            {
                // empty (everything is disabled)
            }
        }
    }
}

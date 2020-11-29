using AdvancedText;
using FunctionAnalyser;
using Microsoft.WindowsAPICodePack.Dialogs;
using IO = System.IO;
using System;
using System.Threading.Tasks;
using System.Windows;
using CommandFilesApi;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using CommandParser;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly AnalyserLogger Logger;
        private string FolderPath;
        private readonly ObservableCollection<TextBlock> Blocks;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();

            Blocks = new ObservableCollection<TextBlock>();
            Output.ItemsSource = Blocks;
            Logger = new AnalyserLogger(Blocks, Output.Dispatcher);

            FolderPath = "";
            TextComponent.SetDefaultColour(Colour.BuiltinColours.WHITE);
            ApiHelper.Initialise();
        }

        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog select = new CommonOpenFileDialog("Select a folder") { IsFolderPicker = true };
            if (select.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = select.FileName;
                AnalyseButton.IsEnabled = true;
            }
        }

        private async void ExportResults(object sender, RoutedEventArgs e)
        {
            CommonSaveFileDialog save = new CommonSaveFileDialog("Export results") { DefaultExtension = "txt", AlwaysAppendDefaultExtension = true };
            save.Filters.Add(new CommonFileDialogFilter("Text Files", "*.txt"));
            if (save.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await IO::File.WriteAllTextAsync(save.FileName, Logger.GetFlatString());
            }
        }

        private async void ReadFiles(object sender, RoutedEventArgs e)
        {
            // Disable options
            MainWindowModel.EnableOptions = false;
            AnalyseButton.IsEnabled = false;
            ExportButton.IsEnabled = false;
            FolderButton.IsEnabled = false;

            // Clear output
            Logger.Clear();

            // Track progress
            Progress<FunctionProgress> progress = new Progress<FunctionProgress>();
            progress.ProgressChanged += ReportProgress;

            // Reading
            FunctionReader functionReader = new FunctionReader(FolderPath, Logger, progress);
            await Task.Run(() =>
            {
                functionReader.Analyse("java");
            });

            // Enable options again
            MainWindowModel.EnableOptions = true;
            AnalyseButton.IsEnabled = true;
            ExportButton.IsEnabled = true;
            FolderButton.IsEnabled = true;
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
                FolderButton.IsEnabled = true;
            } catch (HttpRequestException)
            {
                // empty (everything is disabled)
            }
        }
    }
}

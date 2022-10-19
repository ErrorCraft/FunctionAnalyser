using AdvancedText;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ErrorCraft.FunctionAnalyser.UI.Avalonia.ViewModels;
using ErrorCraft.PackAnalyser;
using ErrorCraft.PackAnalyser.Results;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ErrorCraft.FunctionAnalyser.UI.Avalonia.Views;

public partial class MainWindow : Window {
    private readonly MainWindowViewModel ViewModel;
    private readonly FunctionReader Reader;
    private readonly Progress<FunctionProgress> Progress = new Progress<FunctionProgress>();
    private readonly ILogger Logger;

    public MainWindow() {
        InitializeComponent();
        DataContext = ViewModel = new MainWindowViewModel();
        Logger = new AvaloniaLogger(ViewModel.Output);
        Progress.ProgressChanged += ProgressChanged;
        Reader = new FunctionReader(Logger, Progress);
    }

    private void ProgressChanged(object? sender, FunctionProgress e) { }

    private async void WindowLoaded(object sender, RoutedEventArgs e) {
        try {
            await Reader.LoadVersions();
        } catch (HttpRequestException) { }
    }

    private async void SelectFolder(object sender, RoutedEventArgs e) {
        IReadOnlyList<IStorageFolder> folders = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
        if (folders.Count == 0) {
            return;
        }
        if (!folders[0].TryGetUri(out Uri? uri)) {
            return;
        }
        string path = uri.LocalPath;

        Logger.Clear();

        FunctionOptions options = new FunctionOptions() {
            SkipFunctionOnError = false,
            ShowCommandErrors = true,
            ShowEmptyFunctions = true,
            CommandSortType = SortType.Alphabetical
        };
        await Task.Run(() => Reader.Analyse(path, Reader.GetVersionNames()[0].CommandName, options));
    }
}

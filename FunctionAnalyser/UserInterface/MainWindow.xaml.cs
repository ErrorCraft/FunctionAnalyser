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
using CommandVerifier;
using CommandVerifier.Commands.Collections;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        private readonly TextWriter Writer;
        private string FolderPath;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
            Writer = new TextWriter(Output);
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
                await IO::File.WriteAllTextAsync(save.FileName, Writer.GetFlatOutput());
            }
        }

        private async void ReadFiles(object sender, RoutedEventArgs e)
        {
            MainWindowModel.EnableOptions = false;
            AnalyseButton.IsEnabled = false;
            ExportButton.IsEnabled = false;
            FolderButton.IsEnabled = false;

            // Clear output
            Writer.Reset();

            // Read functions
            Progress<double> progress = new Progress<double>();
            progress.ProgressChanged += ReportProgress;
            FunctionReader functionReader = new FunctionReader(FolderPath, Writer, progress);

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

            MainWindowModel.EnableOptions = true;
            AnalyseButton.IsEnabled = true;
            ExportButton.IsEnabled = true;
            FolderButton.IsEnabled = true;
        }

        private void ReportProgress(object sender, double e)
        {
            Progress.Value = e;
        }

        private async void LoadedWindow(object sender, RoutedEventArgs e)
        {
            await Task.Run(GetFiles);
            FolderButton.IsEnabled = true;
        }

        private async Task GetFiles()
        {
            string commandsJson = await GetFile("commands.json");
            CommandReader.SetCommands(commandsJson);

            string selectorsArgumentsJson = await GetFile("selector_arguments.json");
            EntitySelectorOptions.SetOptions(selectorsArgumentsJson);

            string particlesJson = await GetFile("particles.json");
            Particles.SetOptions(particlesJson);

            string itemsJson = await GetFile("items.json");
            Items.SetOptions(itemsJson);

            string entitiesJson = await GetFile("entities.json");
            Entities.SetOptions(entitiesJson);

            string scoreboardCriteriaJson = await GetFile("scoreboard_criteria.json");
            ScoreboardCriteria.SetOptions(scoreboardCriteriaJson);

            string scoreboardSlotsJson = await GetFile("scoreboard_slots.json");
            ScoreboardSlots.SetOptions(scoreboardSlotsJson);

            string blocksJson = await GetFile("blocks.json");
            Blocks.SetOptions(blocksJson);

            string effectsJson = await GetFile("effects.json");
            Effects.SetOptions(effectsJson);

            string enchantmentsJson = await GetFile("enchantments.json");
            Enchantments.SetOptions(enchantmentsJson);

            Writer.WriteLine(new TextComponent("All done!", Colour.BuiltinColours.GREEN));
        }

        private async Task<string> GetFile(string file)
        {
            Writer.Write(new TextComponent("Getting ", Colour.BuiltinColours.GREY));
            Writer.Write(new TextComponent(file, Colour.BuiltinColours.GOLD));
            Writer.WriteLine(new TextComponent("...", Colour.BuiltinColours.GREY));
            return await FileProcessor.LoadFile(file);
        }
    }
}

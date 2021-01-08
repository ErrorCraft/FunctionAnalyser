using AdvancedText;
using CommandParser;
using CommandParser.Context;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using FunctionAnalyser.Builders;
using FunctionAnalyser.Results;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static AdvancedText.Colour;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        private Dictionary<string, Dispatcher> Versions;

        private string BasePath;
        private readonly System.IProgress<FunctionProgress> Progress;
        private readonly ILogger Logger;
        private FunctionOptions Options;

        public async Task LoadVersions()
        {
            ResourceBuilder resourceBuilder = new ResourceBuilder(Logger);
            Versions = await resourceBuilder.GetResources();
        }

        public List<VersionName> GetVersionNames()
        {
            List<VersionName> versionNames = new List<VersionName>();

            foreach (KeyValuePair<string, Dispatcher> kvp in Versions)
            {
                versionNames.Add(new VersionName(kvp.Key, kvp.Value.GetName()));
            }

            return versionNames;
        }

        public FunctionReader(ILogger logger, System.IProgress<FunctionProgress> progress)
        {
            Logger = logger;
            Progress = progress;
        }

        public void Analyse(string basePath, string version, FunctionOptions options)
        {
            BasePath = basePath;
            Options = options;
            if (!Directory.Exists(BasePath))
            {
                Logger.Log(MessageProvider.FolderDoesNotExist(BasePath));
                return;
            }

            Logger.Log(MessageProvider.AnalyseFunctions(BasePath, Versions[version]));
            Stopwatch timer = Stopwatch.StartNew();
            FunctionData results = AnalyseFunctions(version);
            timer.Stop();

            Logger.Log(MessageProvider.Time(timer));
            Report(results);
        }

        private FunctionData AnalyseFunctions(string version)
        {
            // Results
            FunctionData results = new FunctionData();

            // Get files
            string[] files = Directory.GetFiles(BasePath, "*.mcfunction", SearchOption.AllDirectories);
            int totalFiles = files.Length;

            // Track progress
            FunctionProgress functionProgress = new FunctionProgress();
            Progress.Report(functionProgress);

            // Analyse all files
            for (int i = 0; i < totalFiles; i++)
            {
                if (!File.Exists(files[i])) continue;
                FunctionData functionData = AnalyseFunction(files[i], version);

                functionProgress.Completion = (double)i / totalFiles;
                Progress.Report(functionProgress);

                results += functionData;
                Logger.Log(functionData.Messages);
            }

            // Complete
            functionProgress.Completion = 1.0d;
            Progress.Report(functionProgress);
            return results;
        }

        private FunctionData AnalyseFunction(string path, string version)
        {
            // Data to return
            FunctionData functionData = new FunctionData();

            // Lines
            string[] lines = File.ReadAllLines(path);

            // Errors
            Dictionary<int, CommandError> errors = new Dictionary<int, CommandError>();

            for (int i = 0; i < lines.Length; i++)
            {
                string command = lines[i].Trim();

                if (string.IsNullOrEmpty(command))
                {
                    // Empty line
                    functionData.EmptyLines.Increase();
                    continue;
                } else if (command.StartsWith('#'))
                {
                    // Comment
                    functionData.Comments.Increase();
                    continue;
                } else
                {
                    CommandResults commandResults = Versions[version].Parse(command);
                    if (commandResults.Successful)
                    {
                        functionData.Commands.Increase();
                        // Analyse Arguments
                        bool isFirstArgument = true;
                        for (int j = 0; j < commandResults.Arguments.Count; j++)
                        {
                            AnalyseArgument(commandResults.Arguments[j], functionData, isFirstArgument, false);
                            isFirstArgument = false;
                        }
                    } else
                    {
                        // Error
                        errors.Add(i, commandResults.Error);
                        if (Options.SkipFunctionOnError) break;
                    }
                }
            }

            if (errors.Count > 0)
            {
                if (Options.ShowCommandErrors)
                {
                    functionData.Messages.Add(MessageProvider.ErrorsFound(errors.Count, GetShortFileName(path), Options.SkipFunctionOnError));
                    foreach (KeyValuePair<int, CommandError> kvp in errors)
                    {
                        functionData.Messages.Add(MessageProvider.Error(kvp.Key + 1, kvp.Value));
                    }
                }
                if (Options.SkipFunctionOnError) return functionData;
            } else if (functionData.Commands.GetTotal() == 0)
            {
                if (Options.ShowEmptyFunctions)
                {
                    functionData.Messages.Add(MessageProvider.EmptyFunction(GetShortFileName(path)));
                }
            }

            functionData.Functions.Increase();
            return functionData;
        }

        private static void AnalyseArgument(ParsedArgument argument, FunctionData data, bool firstArgument, bool inSelector)
        {
            object result = argument.GetResult();
            if (argument.IsFromRoot())
            {
                if (result is Literal literal) data.UsedCommands.Increase(literal.Value, !firstArgument);
            }
            else if (result is Function) data.FunctionCalls.Increase();
            else if (result is EntitySelector entitySelector) AnalyseSelector(entitySelector, data);
            else if (result is ScoreHolder scoreHolder && scoreHolder.Selector != null) AnalyseSelector(scoreHolder.Selector, data);
            else if (result is Message message)
            {
                foreach (EntitySelector messageEntitySelector in message.Selectors.Values) AnalyseSelector(messageEntitySelector, data);
            }
            else if (result is Predicate) data.PredicateCalls.Increase(inSelector);
            else if (result is NbtPath) data.NbtAccess.Increase(false);
            else if (result is Nbt && inSelector) data.NbtAccess.Increase(true);
            else if (result is Storage) data.StorageUsage.Increase();
            else if (result is LootTable) data.LootTableUsage.Increase();
            else if (result is ItemModifier) data.ItemModifierUsage.Increase();
            else if (result is Attribute) data.AttributeUsage.Increase();
        }

        private static void AnalyseSelector(EntitySelector entitySelector, FunctionData data)
        {
            // Selector type
            switch (entitySelector.SelectorType)
            {
                case SelectorType.NearestPlayer:
                    data.Selectors.NearestPlayer++;
                    break;
                case SelectorType.AllPlayers:
                    data.Selectors.AllPlayers++;
                    break;
                case SelectorType.RandomPlayer:
                    data.Selectors.RandomPlayer++;
                    break;
                case SelectorType.AllEntities:
                    data.Selectors.AllEntities++;
                    break;
                case SelectorType.Self:
                    data.Selectors.CurrentEntity++;
                    break;
            }

            // Analyse selector arguments
            for (int i = 0; i < entitySelector.Arguments.Count; i++)
            {
                AnalyseArgument(entitySelector.Arguments[i], data, false, true);
            }
        }

        private string GetShortFileName(string file)
        {
            return $".{file[BasePath.Length..]}";
        }

        private void Report(FunctionData data)
        {
            int totalLines = data.Comments.GetTotal() + data.EmptyLines.GetTotal() + data.Commands.GetTotal();
            List<TextComponent> components = new List<TextComponent>
            {
                new TextComponent($"Information:\n").WithColour(BuiltinColours.GREY).WithStyle(false, true),
                MessageProvider.Result($"Number of functions", data.Functions),
                MessageProvider.Result($"Number of comments", data.Comments, totalLines),
                MessageProvider.Result($"Number of empty lines", data.EmptyLines, totalLines),
                MessageProvider.Result($"Number of commands", data.Commands, totalLines)
            };

            foreach (KeyValuePair<string, Command> command in data.UsedCommands.GetSorted(Options.CommandSortType))
            {
                components.Add(MessageProvider.CommandResult(command.Key, command.Value));
            }

            components.Add(new TextComponent("\n").With(MessageProvider.Message($"Selectors:")));
            components.Add(MessageProvider.EntitySelector('p', data.Selectors.NearestPlayer, data.Functions.GetTotal(), data.Commands.GetTotal()));
            components.Add(MessageProvider.EntitySelector('a', data.Selectors.AllPlayers, data.Functions.GetTotal(), data.Commands.GetTotal()));
            components.Add(MessageProvider.EntitySelector('r', data.Selectors.RandomPlayer, data.Functions.GetTotal(), data.Commands.GetTotal()));
            components.Add(MessageProvider.EntitySelector('e', data.Selectors.AllEntities, data.Functions.GetTotal(), data.Commands.GetTotal()));
            components.Add(MessageProvider.EntitySelector('s', data.Selectors.CurrentEntity, data.Functions.GetTotal(), data.Commands.GetTotal()));

            components.Add(MessageProvider.Result($"Function calls", data.FunctionCalls));
            components.Add(MessageProvider.Result($"Predicate calls", data.PredicateCalls));
            components.Add(MessageProvider.Result($"NBT access", data.NbtAccess));
            components.Add(MessageProvider.Result($"Usage of storage", data.StorageUsage));
            components.Add(MessageProvider.Result($"Usage of loot tables", data.LootTableUsage));
            components.Add(MessageProvider.Result($"Usage of item modifiers", data.ItemModifierUsage));
            components.Add(MessageProvider.Result($"Usage of attributes", data.AttributeUsage));

            Logger.Log(components);
        }
    }
}

using CommandParser;
using CommandParser.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        private static Dictionary<string, Dispatcher> Versions;

        private readonly string BasePath;
        private readonly IProgress<FunctionProgress> Progress;

        public static void SetVersions(string json)
        {
            Versions = JsonConvert.DeserializeObject<Dictionary<string, Dispatcher>>(json);
        }

        public FunctionReader(string basePath, IProgress<FunctionProgress> progress)
        {
            BasePath = basePath;
            Progress = progress;
        }

        public async Task AnalyseFunctions(string version)
        {
            string[] files = Directory.GetFiles(BasePath, "*.mcfunction", SearchOption.AllDirectories);
            FunctionProgress functionProgress = new FunctionProgress();
            int totalFiles = files.Length;
            int analysedFiles = 0;

            await Task.Run(() =>
            {
                Parallel.ForEach(files, (file) =>
                {
                    AnalyseFunction(file, version);
                    analysedFiles++;

                    functionProgress.Completion = (double)analysedFiles / totalFiles;
                    Progress.Report(functionProgress);
                });
            });
        }

        private static FunctionData AnalyseFunction(string path, string version)
        {
            FunctionData functionData = new FunctionData();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string command = lines[i].Trim();

                if (string.IsNullOrEmpty(command))
                {
                    // Empty line
                    continue;
                } else if (command.StartsWith('#'))
                {
                    // Comment
                    continue;
                } else
                {
                    CommandResults commandResults = Versions[version].Parse(command);
                    if (commandResults.Successful)
                    {
                        // Analyse Arguments
                    } else
                    {
                        // Error
                    }
                }
            }

            return functionData;
        }

        public static class Options
        {
            public static bool SkipFunctionOnError { get; set; } = false;
            public static bool ShowCommandErrors { get; set; } = true;
            public static bool ShowEmptyFunctions { get; set; } = true;
        }
    }
}

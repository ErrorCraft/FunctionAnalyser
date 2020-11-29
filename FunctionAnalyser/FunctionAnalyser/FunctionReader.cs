﻿using AdvancedText;
using CommandParser;
using CommandParser.Context;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BuiltinTextColours = AdvancedText.Colour.BuiltinColours;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        private static Dictionary<string, Dispatcher> Versions;

        private readonly string BasePath;
        private readonly IProgress<FunctionProgress> Progress;
        private readonly ILogger Logger;

        public static void SetVersions(string json)
        {
            Versions = JsonConvert.DeserializeObject<Dictionary<string, Dispatcher>>(json);
        }

        public FunctionReader(string basePath, ILogger logger, IProgress<FunctionProgress> progress)
        {
            BasePath = basePath;
            Logger = logger;
            Progress = progress;
        }

        public void Analyse(string version)
        {
            if (!Directory.Exists(BasePath))
            {
                Logger.Log(new TextComponent("Folder ").WithColour(BuiltinTextColours.GREY).With(
                    new TextComponent(BasePath).WithColour(BuiltinTextColours.DARK_GREEN).With(
                        new TextComponent($" does not exist!").WithColour(BuiltinTextColours.GREY)
                        )
                    ));
                return;
            }

            Logger.Log(new TextComponent("Analysing all functions in folder ").WithColour(BuiltinTextColours.GREY).With(
                new TextComponent(BasePath).WithColour(BuiltinTextColours.DARK_GREEN).With(
                    new TextComponent($"\nVersion: {Versions[version].GetName()}\n").WithColour(BuiltinTextColours.GREY)
                    )
                ));

            Stopwatch timer = Stopwatch.StartNew();
            FunctionData results = AnalyseFunctions(version);
            timer.Stop();

            Logger.Log(new TextComponent($"Time spent reading: {timer.ElapsedTicks / 10000.0d:0.0000ms}\n").WithColour(BuiltinTextColours.DARK_AQUA).WithStyle(false, true));
            Report(results);
        }

        public FunctionData AnalyseFunctions(string version)
        {
            // Results
            FunctionData results = new FunctionData();

            // Get files
            string[] files = Directory.GetFiles(BasePath, "*.mcfunction", SearchOption.AllDirectories);
            int totalFiles = files.Length;

            // Track progress
            FunctionProgress functionProgress = new FunctionProgress();

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
                    functionData.EmptyLines++;
                    continue;
                } else if (command.StartsWith('#'))
                {
                    // Comment
                    functionData.Comments++;
                    continue;
                } else
                {
                    CommandResults commandResults = Versions[version].Parse(command);
                    if (commandResults.Successful)
                    {
                        functionData.Commands++;
                        // Analyse Arguments
                        for (int j = 0; j < commandResults.Arguments.Count; j++)
                        {
                            AnalyseArgument(commandResults.Arguments[j], functionData);
                        }
                    } else
                    {
                        // Error
                        errors.Add(i, commandResults.Error);
                    }
                }
            }

            if (errors.Count > 0)
            {
                functionData.Messages.Add(MessageProvider.ErrorsFound(errors.Count, GetShortFileName(path), false));
                foreach (KeyValuePair<int, CommandError> kvp in errors)
                {
                    functionData.Messages.Add(MessageProvider.Error(kvp.Key, kvp.Value));
                }
            } else if (functionData.Commands == 0)
            {
                functionData.Messages.Add(MessageProvider.Empty(GetShortFileName(path)));
            }

            functionData.Functions++;
            return functionData;
        }

        private static void AnalyseArgument(ParsedArgument argument, FunctionData data)
        {
            object result = argument.GetResult();
            if (result is Literal)
            {
                data.Literals++;
            }
        }

        private string GetShortFileName(string file)
        {
            return $"..{file[BasePath.Length..]}";
        }

        private static string GetAverage(FunctionData data, int item)
        {
            if (data.Functions == 0) return "";

            StringBuilder stringBuilder = new StringBuilder("(Average: ");
            stringBuilder.Append($"{(double)item / data.Functions:0.00} per file");

            if (data.Commands > 0)
            {
                stringBuilder.Append($", {(double)item / data.Commands:0.00} per command");
            }

            stringBuilder.Append(')');
            return stringBuilder.ToString();
        }

        private void Report(FunctionData data)
        {
            // Dear god
            List<TextComponent> components = new List<TextComponent>
            {
                new TextComponent($"Information:").WithColour(BuiltinTextColours.GREY).WithStyle(false, true),
                new TextComponent($"  Number of functions: {data.Functions}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Number of commands: {data.Commands}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Number of comments: {data.Comments}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Number of empty lines: {data.EmptyLines}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Literals: {data.Literals}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Selectors:").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"    @p: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"    @a: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"    @r: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"    @e: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"    @s: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  NBT Access: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA),
                new TextComponent($"  Predicate Calls: 100 {GetAverage(data, 100)}").WithColour(BuiltinTextColours.AQUA)
            };

            Logger.Log(components);
        }

        public static class Options
        {
            public static bool SkipFunctionOnError { get; set; } = false;
            public static bool ShowCommandErrors { get; set; } = true;
            public static bool ShowEmptyFunctions { get; set; } = true;
        }
    }
}

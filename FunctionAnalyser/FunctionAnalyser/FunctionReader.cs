﻿using AdvancedText;
using CommandVerifier;
using CommandVerifier.Commands;
using System;
using System.Diagnostics;
using System.Text;
using IO = System.IO;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        public FunctionInformation Information { get; private set; }
        private readonly string BasePath;
        private readonly CommandReader Reader;
        private readonly IWriter Output;
        private readonly IProgress<double> Progress;

        public static class Options
        {
            public static bool SkipFunctionOnError { get; set; } = false;
            public static bool ShowCommandErrors { get; set; } = true;
            public static bool ShowEmptyFunctions { get; set; } = true;
        }

        public FunctionReader(string basePath, IWriter output, IProgress<double> progress)
        {
            BasePath = basePath;
            Output = output;
            Reader = new CommandReader();
            Information = new FunctionInformation();
            Progress = progress;
        }

        public void ReadAllFunctions(string version)
        {
            lock (this)
            {
                Output.Write(new TextComponent("Analysing all functions in folder ").WithColour(Colour.BuiltinColours.GREY));
                Output.WriteLine(new TextComponent(BasePath).WithColour(Colour.BuiltinColours.DARK_GREEN));
                Output.WriteLine(new TextComponent("Version: " + CommandReader.GetFancyName(version)).WithColour(Colour.BuiltinColours.GREY));
                Output.WriteLine();

                Stopwatch timer = new Stopwatch();
                timer.Start();
                string[] files = IO::Directory.GetFiles(BasePath, "*.mcfunction", IO::SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    FunctionInformation FileSpecificInformation = new FunctionInformation();
                    string[] lines = IO::File.ReadAllLines(files[i]);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string command = lines[j].Trim();
                        if (command.StartsWith('#'))
                        {
                            FileSpecificInformation.Comments++;
                            continue;
                        }
                        else if (string.IsNullOrEmpty(command))
                        {
                            FileSpecificInformation.EmptyLines++;
                            continue;
                        }
                        else
                        {
                            // Read command
                            StringReader stringReader = new StringReader(command);
                            if (Reader.Parse(version, stringReader))
                            {
                                FileSpecificInformation.Commands += 1;
                                FileSpecificInformation += stringReader.Information;
                            }
                            else if (Options.SkipFunctionOnError) break;
                        }
                    }

                    Progress.Report((double)i/files.Length);

                    if (CommandError.StoredErrors.Count > 0)
                    {
                        if (Options.ShowCommandErrors)
                        {
                            if (CommandError.StoredErrors.Count == 1) Output.Write(new TextComponent("Error found in ").WithColour(Colour.BuiltinColours.RED));
                            else Output.Write(new TextComponent(CommandError.StoredErrors.Count.ToString() + " errors found in ").WithColour(Colour.BuiltinColours.RED));

                            if (Options.SkipFunctionOnError)
                            {
                                Output.Write(".." + files[i].Substring(BasePath.Length));
                                Output.WriteLine(new TextComponent(", skipping function").WithColour(Colour.BuiltinColours.RED));
                                ShowErrors();
                                continue;
                            }
                            else
                            {
                                Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                                ShowErrors();
                            }
                        } else CommandError.StoredErrors.Clear();
                    }
                    else if (Options.ShowEmptyFunctions && FileSpecificInformation.Commands == 0)
                    {
                        Output.Write(new TextComponent("Empty function found at ").WithColour(Colour.BuiltinColours.YELLOW));
                        Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                    }

                    Information.Functions++;
                    Information += FileSpecificInformation;
                }

                timer.Stop();
                Progress.Report(1.0);
                Output.WriteLine(new TextComponent("Time spent reading: " + (timer.ElapsedTicks / 10000.0d).ToString("0.0000") + "ms").WithColour(Colour.BuiltinColours.DARK_AQUA).WithStyle(false, true));
                Output.WriteLine();
                Output.WriteLine(new TextComponent("Information:").WithColour(Colour.BuiltinColours.GREY).WithStyle(false, true));
                Output.WriteLine(new TextComponent("  Number of functions: " + Information.Functions).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of commands: " + Information.Commands).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of comments: " + Information.Comments).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of empty lines: " + Information.EmptyLines).WithColour(Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  Usage of @e selectors: " + Information.EntitySelectors).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.EntitySelectors)).WithColour(Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  NBT Access: " + Information.NbtAccess).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.NbtAccess)).WithColour(Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  Predicate Calls: " + Information.PredicateCalls).WithColour(Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.PredicateCalls)).WithColour(Colour.BuiltinColours.AQUA));
            }
        }

        private string GetAverage(int item)
        {
            if (Information.Functions == 0) return "";

            StringBuilder sb = new StringBuilder("(Average: ");
            sb.Append(((double)item / Information.Functions).ToString("0.00"));
            sb.Append(" per file");

            if (Information.Commands > 0)
            {
                sb.Append(", ");
                sb.Append(((double)item / Information.Commands).ToString("0.00"));
                sb.Append(" per command");
            }

            sb.Append(")");
            return sb.ToString();
        }

        private void ShowErrors()
        {
            for (int i = 0; i < CommandError.StoredErrors.Count; i++)
                Output.WriteLine(new TextComponent("  " + CommandError.StoredErrors[i]).WithColour(Colour.BuiltinColours.RED));
            CommandError.StoredErrors.Clear();
        }
    }
}

using AdvancedText;
using CommandVerifier;
using CommandVerifier.Commands;
using System.Diagnostics;
using IO = System.IO;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        public readonly string BasePath;
        private readonly CommandReader Reader;
        public FunctionInformation Information { get; private set; }
        private readonly IWriter Output;

        public static class Options
        {
            public static bool SkipFunctionOnError { get; set; } = false;
            public static bool ShowCommandErrors { get; set; } = true;
            public static bool ShowEmptyFunctions { get; set; } = true;
        }

        public FunctionReader(string basePath, IWriter output)
        {
            BasePath = basePath;
            Output = output;
            Reader = new CommandReader();
            Information = new FunctionInformation();
        }

        public void ReadAllFunctions(string version)
        {
            lock (this)
            {
                Output.Write(new TextComponent("Analysing all functions in folder ", Colour.BuiltinColours.GREY));
                Output.WriteLine(new TextComponent(BasePath, Colour.BuiltinColours.DARK_GREEN));
                Output.WriteLine(new TextComponent("Version: " + CommandReader.GetFancyName(version), Colour.BuiltinColours.GREY));
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

                    if (CommandError.StoredErrors.Count > 0 && Options.ShowCommandErrors)
                    {
                        if (CommandError.StoredErrors.Count == 1) Output.Write(new TextComponent("Error found in ", Colour.BuiltinColours.RED));
                        else Output.Write(new TextComponent(CommandError.StoredErrors.Count.ToString() + " errors found in ", Colour.BuiltinColours.RED));

                        if (Options.SkipFunctionOnError)
                        {
                            Output.Write(".." + files[i].Substring(BasePath.Length));
                            Output.WriteLine(new TextComponent(", skipping function", Colour.BuiltinColours.RED));
                            ShowErrors();
                            continue;
                        }
                        else
                        {
                            Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                            ShowErrors();
                        }
                    }
                    else if (Options.ShowEmptyFunctions && FileSpecificInformation.Commands == 0)
                    {
                        Output.Write(new TextComponent("Empty function found at ", Colour.BuiltinColours.YELLOW));
                        Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                    }

                    Information.Functions++;
                    Information += FileSpecificInformation;
                }

                timer.Stop();
                Output.WriteLine(new TextComponent("Time spent reading: " + (timer.ElapsedTicks / 10000.0d).ToString("0.0000") + "ms", Colour.BuiltinColours.DARK_AQUA, false, true));
                Output.WriteLine();
                Output.WriteLine(new TextComponent("Information:", Colour.BuiltinColours.BLACK, false, true));
                Output.WriteLine(new TextComponent("  Number of functions: " + Information.Functions, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of commands: " + Information.Commands, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of comments: " + Information.Comments, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("  Number of empty lines: " + Information.EmptyLines, Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  Usage of @e selectors: " + Information.EntitySelectors, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.EntitySelectors), Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  NBT Access: " + Information.NbtAccess, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.NbtAccess), Colour.BuiltinColours.AQUA));

                Output.Write(new TextComponent("  Predicate Calls: " + Information.PredicateCalls, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent(" " + GetAverage(Information.PredicateCalls), Colour.BuiltinColours.AQUA));
            }
        }

        private string GetAverage(int information)
        {
            return "(Average: " +
                ((double)information / Information.Functions).ToString("0.00") + " per file, " +
                ((double)information / Information.Commands).ToString("0.00") + " per command)";
        }

        private void ShowErrors()
        {
            for (int i = 0; i < CommandError.StoredErrors.Count; i++)
                Output.WriteLine(new TextComponent("  " + CommandError.StoredErrors[i], Colour.BuiltinColours.RED));
            CommandError.StoredErrors.Clear();
        }
    }
}

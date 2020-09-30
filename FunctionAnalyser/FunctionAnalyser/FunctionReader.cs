using AdvancedText;
using CommandVerifier;
using CommandVerifier.Commands;
using System.Diagnostics;
using System.IO;

namespace FunctionAnalyser
{
    public class FunctionReader
    {
        public readonly string BasePath;
        private readonly CommandReader commandReader;
        public FunctionInformation Information { get; private set; }
        private readonly IWriter Output;

        public FunctionReader(string basePath, IWriter output)
        {
            BasePath = basePath;
            Output = output;
            commandReader = new CommandReader();
            Information = new FunctionInformation();
        }

        public void ReadAllFunctions(string version)
        {
            lock (this)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                string[] files = Directory.GetFiles(BasePath, "*.mcfunction", SearchOption.AllDirectories);
                Information.Functions = files.Length;
                for (int i = 0; i < files.Length; i++)
                {
                    FunctionInformation FileSpecificInformation = new FunctionInformation();
                    string[] lines = File.ReadAllLines(files[i]);
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
                            CommandVerifier.StringReader stringReader = new CommandVerifier.StringReader(command);
                            if (commandReader.Parse(version, stringReader))
                            {
                                FileSpecificInformation.Commands += 1;
                                FileSpecificInformation += stringReader.Information;
                            }
                            else
                            {
                                Output.Write(new TextComponent("Errors found in ", Colour.BuiltinColours.DARK_RED));
                                Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                                foreach (string error in CommandError.StoredErrors)
                                    Output.WriteLine(new TextComponent(error, Colour.BuiltinColours.DARK_RED));
                                CommandError.StoredErrors.Clear();
                                FileSpecificInformation.Reset();
                                break;
                            }
                        }
                    }
                    Information += FileSpecificInformation;
                }

                timer.Stop();
                Output.WriteLine(new TextComponent("Time spent reading: " + (timer.ElapsedTicks / 10000.0d).ToString("0.0000") + "ms", Colour.BuiltinColours.AQUA, false, true));
                Output.WriteLine(new TextComponent("Number of functions: " + Information.Functions, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("Number of commands: " + Information.Commands, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("Number of comments: " + Information.Comments, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("Number of empty lines: " + Information.EmptyLines, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("Usage of @e selectors: " + Information.EntitySelectors, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("NBT Access: " + Information.NbtAccess, Colour.BuiltinColours.AQUA));
                Output.WriteLine(new TextComponent("Predicate Calls: " + Information.PredicateCalls, Colour.BuiltinColours.AQUA));
            }
        }
    }
}

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
                                Output.Write(new TextComponent("Errors found in ", "dark_red"));
                                Output.WriteLine(".." + files[i].Substring(BasePath.Length));
                                foreach (string error in CommandError.StoredErrors)
                                    Output.WriteLine(new TextComponent(error, "dark_red"));
                                CommandError.StoredErrors.Clear();
                                FileSpecificInformation.Reset();
                                break;
                            }
                        }
                    }
                    Information += FileSpecificInformation;
                }

                timer.Stop();
                Output.WriteLine(new TextComponent("Time spent reading: " + (timer.ElapsedTicks / 10000.0d).ToString("0.0000") + "ms", "aqua", false, true));
                Output.WriteLine(new TextComponent("Number of functions: " + Information.Functions, "aqua"));
                Output.WriteLine(new TextComponent("Number of commands: " + Information.Commands, "aqua"));
                Output.WriteLine(new TextComponent("Number of comments: " + Information.Comments, "aqua"));
                Output.WriteLine(new TextComponent("Number of empty lines: " + Information.EmptyLines, "aqua"));
                Output.WriteLine(new TextComponent("Usage of @e selectors: " + Information.EntitySelectors, "aqua"));
                Output.WriteLine(new TextComponent("NBT Access: " + Information.NbtAccess, "aqua"));
                Output.WriteLine(new TextComponent("Predicate Calls: " + Information.PredicateCalls, "aqua"));

                Output.Write(new TextComponent("a", "dark_blue"));
                Output.Write(new TextComponent("a", "dark_green"));
                Output.Write(new TextComponent("a", "dark_aqua"));
                Output.Write(new TextComponent("a", "dark_red"));
                Output.Write(new TextComponent("a", "dark_purple"));
                Output.Write(new TextComponent("a", "gold"));
                Output.Write(new TextComponent("a", "grey"));
                Output.Write(new TextComponent("a", "dark_grey"));
                Output.Write(new TextComponent("a", "blue"));
                Output.Write(new TextComponent("a", "green"));
                Output.Write(new TextComponent("a", "aqua"));
                Output.Write(new TextComponent("a", "red"));
                Output.Write(new TextComponent("a", "light_purple"));
                Output.Write(new TextComponent("a", "yellow"));
                Output.Write(new TextComponent("a", "white"));
                Output.Write(new TextComponent("a\n", "black"));

                Output.WriteLine(new TextComponent("hell", "dark_red"));
                Output.WriteLine(new TextComponent("low", "yellow"));
                Output.WriteLine(new TextComponent("world", "aqua"));

                //Output.WriteLine(new TextComponent("Error", "dark_red"));
                //Output.WriteLine(new TextComponent("Warning", "yellow"));
                //Output.WriteLine(new TextComponent("Information", "aqua"));
            }
        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands
{
    class CommandCollection
    {
        [JsonProperty("version")]
        public string Version { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("commands")]
        public List<Command> Commands { get; }

        [JsonConstructor]
        public CommandCollection()
        {
            Version = "";
            Name = "";
            Commands = new List<Command>();
        }

        public bool Parse(StringReader reader)
        {
            while (reader.Data.ExpectCommand)
            {
                reader.Data.Reset();
                int index = -1;
                int start = reader.Cursor;
                for (int i = 0; i < Commands.Count; i++)
                {
                    if (Commands[i].CommandMatches(reader))
                    {
                        index = i;
                        reader.Data.ExpectCommand = false;
                        break;
                    }
                    reader.SetCursor(start);
                }

                if (index > -1)
                {
                    // Was able to parse
                    if (Commands[index].Parse(reader))
                    {
                        // Out of loop, expect another command
                        if (Commands[index].LoopContents && reader.Data.ExpectCommand) continue;

                        // Is not at the end of the command
                        if (reader.CanRead())
                        {
                            CommandError.IncorrectArgument().AddWithContext(reader);
                            return false;
                        }
                        return true;
                    }
                    return false;
                }

                // No command found
                break;
            }

            CommandError.UnknownOrIncompleteCommand().AddWithContext(reader);
            return false;
        }
    }
}

using CommandVerifier.Commands.Converters;
using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands
{
    class Command
    {
        [JsonProperty("command", Required = Required.Always)]
        public string command { get; set; }

        [JsonConverter(typeof(SubcommandConverter))]
        [JsonProperty("contents", Required = Required.Always)]
        public Subcommand Contents { get; set; }

        [JsonProperty("loop_contents")]
        [DefaultValue(false)]
        public bool LoopContents { get; set; }

        [JsonProperty("aliases")]
        public string[] Aliases { get; set; }

        public bool CommandMatches(StringReader reader)
        {
            int start = reader.Cursor;
            if (reader.CanRead(command.Length))
            {
                string s = reader.Read(command.Length);
                if (s == command && reader.IsEndOfArgument())
                {
                    if (reader.CanRead()) reader.Skip();
                    return true;
                }
            }

            // Aliases
            if (Aliases != null)
            {
                for (int i = 0; i < Aliases.Length; i++)
                {
                    reader.SetCursor(start);
                    if (reader.CanRead(Aliases[i].Length))
                    {
                        string s = reader.Read(Aliases[i].Length);
                        if (s == Aliases[i] && reader.IsEndOfArgument())
                        {
                            if (reader.CanRead()) reader.Skip();
                            return true;
                        }
                    }
                    continue;
                }
            }
            return false;
        }

        public bool Parse(StringReader reader)
        {
            if (Contents == null) return true;
            reader.commandData.LoopContents = LoopContents;
            if (LoopContents)
            {
                while (!reader.commandData.EscapeLoop)
                {
                    if (!Contents.Check(reader, true)) return false;
                    // add param for this check? (in both contents root and CommandData)
                    //if (reader.commandData.ExpectCommand && reader.CanRead()) reader.Skip();
                    //if ((!reader.commandData.EscapeLoop || reader.commandData.ExpectCommand) && reader.CanRead()) reader.Skip();
                }
                return true;
            }
            return Contents.Check(reader, true);
        }
    }
}

using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    public class Subcommand
    {
        [JsonProperty("optional")]
        [DefaultValue(false)]
        public bool Optional { get; set; }

        [JsonProperty("may_escape_loop")]
        [DefaultValue(false)]
        public bool MayEscapeLoop { get; set; }

        [JsonProperty("expect_command")]
        [DefaultValue(false)]
        public bool ExpectCommand { get; set; }

        [JsonProperty("may_be_skipped")]
        [DefaultValue(false)]
        public bool MayBeSkipped { get; set; }

        private protected void SetLoopAttributes(StringReader reader)
        {
            reader.Data.PassedFirstRequirement = !reader.Data.DisableForcedPath;
            if (MayEscapeLoop)
            {
                if (ExpectCommand) // new command
                {
                    reader.Data.EscapeLoop = true;
                    reader.Data.ExpectCommand = true;
                } else if (reader.CanRead()) // keep on reading
                {
                    reader.Data.EscapeLoop = false;
                    reader.Data.ExpectCommand = false;
                } else // stop reading
                {
                    reader.Data.EscapeLoop = true;
                    reader.Data.ExpectCommand = false;
                }
            }
        }

        public virtual bool Check(StringReader reader, bool throw_on_fail) { return true; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Requirements : SubcommandCollection
    {
        [JsonProperty("disable_forced_path")]
        [DefaultValue(false)]
        public bool DisableForcedPath { get; set; }

        [JsonProperty("force_end_of_command")]
        [DefaultValue(false)]
        public bool ForceEndOfCommand { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            // Check all
            reader.Data.DisableForcedPath = DisableForcedPath;
            reader.Data.PassedAllRequirements = false;
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i].MayBeSkipped)
                {
                    int start = reader.Cursor;
                    if (!Values[i].Check(reader, false))
                    {
                        reader.SetCursor(start);
                        continue;
                    }
                } else
                {
                    // First iteration, or disabled force
                    if (i == 0 || DisableForcedPath)
                    {
                        // Check failed
                        if (!Values[i].Check(reader, throw_on_fail)) return false;
                    }
                    // Check failed
                    else if (!Values[i].Check(reader, true)) return false;
                }

                // Ended with optional
                if (reader.Data.EndedOptional) return true;

                if (!reader.Data.PassedAllRequirements)
                {
                    // Argument separator
                    if (!reader.IsEndOfArgument())
                    {
                        if (throw_on_fail || i > 0) CommandError.ExpectedArgumentSeparator().AddWithContext(reader);
                        return false;
                    }

                    // Skip
                    if (reader.CanRead()) reader.Skip();
                }

                // Reset passing all requirements (in case of sub-requirements)
                reader.Data.PassedAllRequirements = false;
            }

            //if (ForceEndOfCommand && reader.CanRead()) return false;

            reader.Data.PassedAllRequirements = true;
            return true;
        }
    }
}

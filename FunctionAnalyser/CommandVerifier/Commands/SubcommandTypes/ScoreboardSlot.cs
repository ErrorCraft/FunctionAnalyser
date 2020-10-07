using CommandVerifier.Commands.Collections;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class ScoreboardSlot : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            if (!reader.TryReadUnquotedString(out string result)) return false;
            string[] values = result.Split('.');
            if (ScoreboardSlots.Options.ContainsKey(values[0]))
            {
                if (ScoreboardSlots.Options[values[0]].TryRead(result.Substring(values[0].Length)))
                {
                    SetLoopAttributes(reader);
                    return true;
                }
            }

            if (throw_on_fail) CommandError.UnknownDisplaySlot(result).Add();
            return false;
        }
    }
}

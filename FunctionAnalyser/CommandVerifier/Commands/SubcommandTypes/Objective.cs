namespace CommandVerifier.Commands.SubcommandTypes
{
    class Objective : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            if (!reader.TryReadUnquotedString(out string value)) return false;

            if (value.Length > 16)
            {
                if (throw_on_fail) CommandError.ObjectiveNameTooLong().Add();
                return false;
            }
            SetLoopAttributes(reader);
            return true;
        }
    }
}

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Boolean : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }
            if (reader.TryReadBoolean(throw_on_fail, out _))
            {
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}

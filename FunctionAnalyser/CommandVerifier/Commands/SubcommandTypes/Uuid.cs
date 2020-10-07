namespace CommandVerifier.Commands.SubcommandTypes
{
    class Uuid : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            if (reader.TryReadUuid(throw_on_fail, out _))
            {
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}

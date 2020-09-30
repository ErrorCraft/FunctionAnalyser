namespace CommandVerifier.Commands.SubcommandTypes.Coordinates
{
    class Vec3 : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            if (reader.CanRead() && reader.Peek() == '^')
            {
                if (!LocalCoordinates.TryRead(reader, throw_on_fail)) return false;
                SetLoopAttributes(reader);
                return true;
            }

            if (!WorldCoordinates.TryReadDouble(reader, throw_on_fail)) return false;
            SetLoopAttributes(reader);
            return true;
        }
    }
}

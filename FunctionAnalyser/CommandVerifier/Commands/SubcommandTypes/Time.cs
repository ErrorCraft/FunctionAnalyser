namespace CommandVerifier.Commands.SubcommandTypes
{
    class Time : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;
            if (!reader.TryReadFloat(throw_on_fail, out float result)) return false;

            if (result < 0.0f)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.InvalidTickCount().Add();
                return false;
            }

            if (!reader.IsEndOfArgument())
            {
                char c = reader.Peek();
                if (!(c == 'd' || c == 's' || c == 't'))
                {
                    if (throw_on_fail) CommandError.InvalidTimeUnit().AddWithContext(reader);
                    return false;
                }
                reader.Skip();
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
